using System;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace rabbitmq.rpc_search_handler
{
<<<<<<< HEAD:Backend/Kulturarv_Final_Project_PBA/SearchService/rabbitmq/rpc_search_handler/Search_Rpc.cs
public class RPCServer
{
    private readonly IConfiguration _config;
    public RPCServer(IConfiguration config)
    {
        _config = config;
		while(true)
        Listen_And_Respond();
    }
=======
	private readonly IConnection connection;
    private readonly IModel channel;
    private string replyQueueName;
    private EventingBasicConsumer consumer;
    private readonly BlockingCollection<string> respQueue = new BlockingCollection<string>();
    private IBasicProperties props;
	public Search_Rpc()
	{
		Init(); 
	}

	//TODO: Call the service. 
	
	private void Init()
	{
		Console.WriteLine("Search_Rpc init!");
		var factory = new ConnectionFactory();	//TODO: --> Need URL.
		using(var connection = factory.CreateConnection())
		{
			using(var channel = connection.CreateModel())
			{
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
>>>>>>> developer:Backend/Kulturarv_Final_Project_PBA/SearchService/api/rabbitmq/rpc_search_handler/Search_Rpc.cs

    private ConnectionFactory Setup_Connection_Factory()
    {
		var rabbitMqConfig = _config.GetSection("RabbitMqConnection").Get<RabbitMQConnectionHandler>();
		var factory = new ConnectionFactory
		{
			HostName = rabbitMqConfig.HostName,
			Password = rabbitMqConfig.Password,
			VirtualHost = rabbitMqConfig.VirtualHost,
			UserName = rabbitMqConfig.Username,
			Uri = new Uri(rabbitMqConfig.URL),
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
                    response = "awds";
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