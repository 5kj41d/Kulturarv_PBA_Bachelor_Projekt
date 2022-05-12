using System;
using System.Collections.Concurrent;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

public class Search_Rpc
{
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

				//NOTE: Should this be done in the gateways?
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
		}

	}

	//TODO: Should be called?
	public string Call(string message)
    {
        var messageBytes = Encoding.UTF8.GetBytes(message);
        channel.BasicPublish(
            exchange: "",
            routingKey: "rpc_queue",
            basicProperties: props,
            body: messageBytes);

        return respQueue.Take();
    }

    public void Close()
    {
        connection.Close();
    }
	
}