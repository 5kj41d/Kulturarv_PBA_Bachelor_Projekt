using System;
using RabbitMQ.Client;
using System.Text;
using RabbitMQ.Client.Events;
using System.Collections.Concurrent;

namespace rabbitmq
{
    //Ressource: https://www.rabbitmq.com/tutorials/tutorial-six-dotnet.html 
    public interface RabbitMqRPCIF
    {
        
    }
    public class RabbitMqRPC : RabbitMqRPCIF
    {
        public RabbitMqRPC()
        {
            Init(); 
        }

        private void Init()
        {
            var factory = new ConnectionFactory() {HostName = ""};  //TODO: Need config file!
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "RPC_Request_Search_Queue", durable: true,
                exclusive: false, autoDelete: false, arguments: null);
                channel.BasicQos(0,1,false);
                var consumer = new EventingBasicConsumer(channel);
                channel.BasicConsume(queue: "RPC_Reply_Search_Queue", autoAck: false, consumer: consumer);
                Console.WriteLine(" [x] Awaiting RPC requests");

                consumer.Received += (model, ea) => 
                {
                    string response = null; 
                    var body = ea.Body.ToArray(); 
                    var props = ea.BasicProperties; 
                    var replyProps = channel.CreateBasicProperties(); 
                    replyProps.CorrelationId = props.CorrelationId; 

                    try
                    {
                        var message = Encoding.UTF8.GetString(body);
                        Console.WriteLine($"Message: {message}");  
                        response = Make_Request_And_Get_Response(message);
                    } 
                    catch(Exception e) 
                    {
                        //TODO: LOG ERROR!
                        Console.WriteLine("ERROR! " + e.Message);
                        response = "";   
                    } 
                    finally 
                    {   
                        //SENDING RESPONSE BACK TO SENDER: 
                        var responseBytes = Encoding.UTF8.GetBytes(response);
                        channel.BasicPublish(exchange: "amq.direct", routingKey: "Search", basicProperties: replyProps, body: responseBytes);
                        channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                    }
                }; 
            } 
        }

        private string Make_Request_And_Get_Response(string message)
        {
            //TODO: Should be routed and sent to the right micro service using rabbitmq. 
            ServiceReposIF repos = new ServiceRepos();  
            return repos.Call(message); 
        }










        //TODO: Should be moved to its own file later on.
        public interface ServiceReposIF
        {
            public string SearchHeritageSite(string message);
            //DEMO METHOD BELOW:
            public string Call(string message);
        }
        //TODO: Should be moved to its own file later on.
        public class ServiceRepos : ServiceReposIF
        {
            private readonly IConnection connection;
            private readonly IModel channel;
            private readonly string replyQueueName;
            private readonly EventingBasicConsumer consumer;
            private readonly BlockingCollection<string> respQueue = new BlockingCollection<string>();
            private readonly IBasicProperties props;
            public ServiceRepos()
            {
                var factory = new ConnectionFactory() { HostName = "localhost" };       //TODO: Add config file to handle this!

                connection = factory.CreateConnection();
                channel = connection.CreateModel();
                replyQueueName = channel.QueueDeclare().QueueName;
                consumer = new EventingBasicConsumer(channel);

                props = channel.CreateBasicProperties();
                var correlationId = Guid.NewGuid().ToString();
                props.CorrelationId = correlationId;
                props.ReplyTo = replyQueueName;

                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var response = Encoding.UTF8.GetString(body);
                    if (ea.BasicProperties.CorrelationId == correlationId)
                    {
                      respQueue.Add(response);
                    }
                };

                channel.BasicConsume(
                    consumer: consumer,
                    queue: replyQueueName,
                    autoAck: true);
            }
            public string Call(string message)
            {
                var messageBytes = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(
                    exchange: "amq.direct",
                    routingKey: "RPC_Request_Search_MicroService_Queue",
                    basicProperties: props,
                    body: messageBytes);

                return respQueue.Take();
            }   

            public void Close()
            {
                connection.Close();
            }


            //TODO: MAKE THIS THE MAIN METHOD LATER FOR GETTING DATA!
            public string SearchHeritageSite(string message)
            {
                return ""; 
            }
        }
    }
}