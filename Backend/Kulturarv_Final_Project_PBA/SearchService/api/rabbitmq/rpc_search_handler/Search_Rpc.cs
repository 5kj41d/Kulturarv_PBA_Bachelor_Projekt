using System;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.Collections.Concurrent;

namespace rabbitmq.rpc_search_handler
{
public class Search_Rpc
{
    private readonly IConfiguration _config;
	private readonly IConnection connection;
    private readonly IModel channel;
    private string replyQueueName;
    private EventingBasicConsumer consumer;
    private readonly BlockingCollection<string> respQueue = new BlockingCollection<string>();
    private IBasicProperties props;
	public Search_Rpc(IConfiguration config)
	{
			ConnectionFactory factory = new ConnectionFactory();
			factory.UserName = "qrnmzmvv";
			factory.Password = "acgnVEp0crbkjhO_iySLLAxSFV-CiK3J";
			factory.Port = 5672;
			factory.VirtualHost = "qrnmzmvv";
			factory.HostName = "hawk.rmq.cloudamqp.com";
			connection = factory.CreateConnection();
			channel = connection.CreateModel();
			replyQueueName = channel.QueueDeclare().QueueName;
			consumer = new EventingBasicConsumer(channel);
			//Event when it receives message
			consumer.Received += (model, ea) =>
			{
				var body = ea.Body.ToArray();
				var message = Encoding.UTF8.GetString(body);
			};
			
			
			//Consumes from channel declared in the queue name
			channel.BasicConsume
			(
				queue: "RPC_Request_From_Aggregator",
				autoAck: true,
				consumer: consumer
			) ; ;
			
			//Init(); 
		}

		//TODO: Call the service. 

		private void Init()
		{
			Console.WriteLine("Search_Rpc init!");
			var factory = new ConnectionFactory();  //TODO: --> Need URL.
			factory.UserName = "qrnmzmvv";
			factory.VirtualHost = "qrnmzmvv";
			factory.Password = "acgnVEp0crbkjhO_iySLLAxSFV-CiK3J";
			factory.HostName = "hawk.rmq.cloudamqp.com";
			using (var connection = factory.CreateConnection())
			{
				using (var channel = connection.CreateModel())
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
				}
			}
		}

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
    

    private void Check_Message_Body()
    {
        //TODO: Check hvilken metode den skal kalde. 
    }
}
}