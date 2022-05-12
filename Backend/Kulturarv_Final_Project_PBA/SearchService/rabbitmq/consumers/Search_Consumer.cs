using System;
using System.Text;
using RabbitMQ.Client;

public class Search_Consumer : DefaultBasicConsumer
{
	private readonly IModel _channel; 	
	public Search_Consumer(IModel channel){
		_channel = channel; 
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