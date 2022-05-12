using System;
using System.Text;
using RabbitMQ.Client;

public class Search_Gatway_Request_Consumer : DefaultBasicConsumer
{
	//TODO: Log: Id, Beskedinhold, bruger, tidspunktet, Log_årsag.

	//TODO: Should not be hardcoded.
	private const string UserName = "guest";
    private const string Password = "guest";
    private const string HostName = "localhost";
	private readonly IModel _channel; 	
	public Search_Gatway_Request_Consumer()
	{
		Init(); 
	}

	private void Init()
	{
		ConnectionFactory connectionFactory = new ConnectionFactory
            {
                HostName = HostName,
                UserName = UserName,
                Password = Password,
            };
            var connection = connectionFactory.CreateConnection();
            var _channel = connection.CreateModel();
            // accept only one unack-ed message at a time
            // uint prefetchSize, ushort prefetchCount, bool global
            _channel.BasicQos(0, 1, false);
            _channel.BasicConsume("search_consumer_queue", false, this);
            Console.ReadLine();
	}

    public override void HandleBasicDeliver(string consumerTag, ulong deliveryTag, bool redelivered, string exchange, 
	string routingKey, IBasicProperties properties, ReadOnlyMemory<byte> body)
    {
		Console.WriteLine($"Consuming Message");
        Console.WriteLine(string.Concat("Message received from the exchange ", exchange));
        Console.WriteLine(string.Concat("Consumer tag: ", consumerTag));
        Console.WriteLine(string.Concat("Delivery tag: ", deliveryTag));
        Console.WriteLine(string.Concat("Routing tag: ", routingKey));
        Console.WriteLine(string.Concat("Message: ", Encoding.UTF8.GetString(body.Span)));
		_channel.BasicAck(deliveryTag, false);
		Forward_Message_To_Search_Engine(body.ToString());
	}
    private void Forward_Message_To_Search_Engine(string message)
	{
		//TODO: Sent message to search engine. Log if some error occurs. 
	}
	private void Reject_Message()
	{
		//TODO: Log these. 
	}
}