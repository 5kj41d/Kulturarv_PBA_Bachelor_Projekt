using System;
using RabbitMQ.Client;
using System.Text;
using RabbitMQ.Client.Events;

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
            var factory = new ConnectionFactory() {HostName = ""}; 
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
            return "THIS IS A HERITAGE SITE!"; 
        }
    }
}