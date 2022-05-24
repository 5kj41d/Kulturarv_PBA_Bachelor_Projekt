using System;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace rabbitmq.rpc_search_handler
{
public class RPCServer
{
    private readonly IConfiguration _config;
    public RPCServer(IConfiguration config)
    {
        _config = config;
        Listen_And_Respond();
    }

    private ConnectionFactory Setup_Connection_Factory()
    {
		var rabbitMqConfig = _config.GetSection("RabbitMqConnection").Get<RabbitMQConnectionHandler>();
		var factory = new ConnectionFactory
		{
			HostName = rabbitMqConfig.HostName,
			Password = rabbitMqConfig.Password,
			VirtualHost = rabbitMqConfig.VirtualHost,
			UserName = rabbitMqConfig.Username
		};
        return factory;
    }
    public void Listen_And_Respond()
    {
        var factory = Setup_Connection_Factory();
        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            channel.QueueDeclare(queue: "rpc_queue", durable: false,
              exclusive: false, autoDelete: false, arguments: null);
            channel.BasicQos(0, 1, false);
            var consumer = new EventingBasicConsumer(channel);
            channel.BasicConsume(queue: "rpc_queue",
              autoAck: false, consumer: consumer);
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
                    //int n = int.Parse(message);
                    //Console.WriteLine(" [.] fib({0})", message);
                    //response = fib(n).ToString(); --> Method of our choice. 
                }
                catch (Exception e)
                {
                    Console.WriteLine(" [.] " + e.Message);
                    response = "";
                }
                finally
                {
                    var responseBytes = Encoding.UTF8.GetBytes(response);
                    channel.BasicPublish(exchange: "", routingKey: props.ReplyTo,
                      basicProperties: replyProps, body: responseBytes);
                    channel.BasicAck(deliveryTag: ea.DeliveryTag,
                      multiple: false);
                }
            };
        }
    }

    private void Check_Message_Body()
    {
        //TODO: Check hvilken metode den skal kalde. 
    }
}
}