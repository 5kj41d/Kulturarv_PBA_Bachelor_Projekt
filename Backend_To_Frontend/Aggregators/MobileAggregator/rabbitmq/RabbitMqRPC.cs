using System;
using RabbitMQ.Client;
using System.Text;
using RabbitMQ.Client.Events;
using System.Collections.Concurrent;
using System.Collections.Generic;

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
            //TODO: Need config file!
            var factory = new ConnectionFactory() {HostName = RabbitMqConenction.HostName, 
                UserName = RabbitMqConenction.Username, Password = RabbitMqConenction.Password, 
                VirtualHost = RabbitMqConenction.VirtualHost};  
            using (var connection = factory.CreateConnection())
            using(var con_channel = connection.CreateModel())
            using (var pub_channel = connection.CreateModel())
            {
                pub_channel.QueueDeclare(queue: RabbitMqConenction.Channels["RPC_Request_From_Aggregator"], durable: true,
                exclusive: false, autoDelete: false, arguments: null);
                pub_channel.BasicQos(0,1,false);
                var consumer = new EventingBasicConsumer(pub_channel);
                con_channel.BasicConsume(queue: RabbitMqConenction.Channels["RPC_Reply"], autoAck: false, consumer: consumer);

                consumer.Received += (model, ea) => 
                {
                    string response = null; 
                    var body = ea.Body.ToArray(); 
                    var props = ea.BasicProperties; 
                    var replyProps = pub_channel.CreateBasicProperties(); 
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
                        pub_channel.BasicPublish(exchange: "amq.direct", routingKey: "Search", basicProperties: replyProps, body: responseBytes);
                        pub_channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
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

        public static class RabbitMqConenction
        {
            public static string HostName = "hawk.rmq.cloudamqp.com";
            public static string Username = "qrnmzmvv";
            public static string Password = "acgnVEp0crbkjhO_iySLLAxSFV-CiK3J";
            public static string VirtualHost = "/";
            public static List<string> Routing_Keys = new List<string>{"Search", "Auth", "Map_Service"};
            public static Dictionary<string,string> Channels = new Dictionary<string, string>{
                {"RPC_Reply","RPC_Reply_Search_Queue"},
                {"RPC_Request","RPC_Request_Search_Queue"},
                {"Auth_Reply","Authentication_Queue_Reply"},
                {"Auth_Request","Authentication_Queue_Request"},
                {"RPC_Request_From_Aggregator", "RPC_Request_From_Aggregator"}
            };
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