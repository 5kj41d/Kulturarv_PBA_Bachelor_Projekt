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
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: RabbitMqConenction.Channels["RPC_Request_From_Aggregator"], durable: true,
                exclusive: false, autoDelete: false, arguments: null);
                channel.BasicQos(0,1,false);
                var consumer = new EventingBasicConsumer(channel);
                channel.BasicConsume(queue: RabbitMqConenction.Channels["RPC_Reply"], autoAck: false, consumer: consumer);

                consumer.Received += (model, ea) => 
                {
                    var body = ea.Body.ToArray(); 
                    var props = ea.BasicProperties; 
                    var replyProps = channel.CreateBasicProperties(); 
                    replyProps.CorrelationId = props.CorrelationId;
                    var message = "";  

                    try
                    {
                        message = Encoding.UTF8.GetString(body);
                        Console.WriteLine($"Message: {message}");  
                    } 
                    catch(Exception e) 
                    {
                        //TODO: LOG ERROR!
                        Console.WriteLine("ERROR! " + e.Message);
                        message = "";   
                    } 
                    finally 
                    {   
                        //SENDING RESPONSE BACK TO SENDER: 
                        var responseBytes = Encoding.UTF8.GetBytes(message);
                        channel.BasicPublish(exchange: "amq.direct", routingKey: "Search", basicProperties: replyProps, body: responseBytes);
                        channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                    }
                }; 
            } 
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
                
    }

            public string SearchHeritageSite(string message)
            {
                throw new NotImplementedException();
            }
        }
    }
}