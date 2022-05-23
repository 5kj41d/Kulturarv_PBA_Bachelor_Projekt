using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Collections.Concurrent;
using System.Text; 

namespace Mobile_Gateway.rabbitmq
{
//Interface 
public interface Rpc_sender_IF 
{
    public string Sent_Message_To_Message_Bus_RPC(Sent_Model sent_model);
    public bool Test_Connection();
}
/////

public class Rpc_sender : Rpc_sender_IF
{
    private readonly ILogger _logger; 
    private readonly RabbitMqConfiguration _configuration; 
    private string replyQueueName;
    private readonly EventingBasicConsumer consumer;
    private readonly BlockingCollection<string> respQueue = new BlockingCollection<string>();

    public Rpc_sender(IOptions<RabbitMqConfiguration> options, ILogger logger)
    {
        _configuration = options.Value; 
        _logger = logger; 
        Init(); 
    }

    /// <summary>
    /// Initiate consumer/response channel. 
    /// </summary>
    private void Init()
    {
        IModel channel; 
        var conn = Setup_Connection();
        channel = conn.CreateModel();
        replyQueueName = channel.QueueDeclare().QueueName;
        var consumer = new EventingBasicConsumer(channel);

        IBasicProperties props = channel.CreateBasicProperties();
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

    /// <summary> 
    /// Version 1. Takes a string of search message to be sent. Returns void. 
    /// </summary>
    /// param name="message" of string
    public string Sent_Message_To_Message_Bus_RPC(Sent_Model sent_model)
    {
        string correlation_id = Guid.NewGuid().ToString();
        IConnection conn = null;
        IModel channel = null;  
        try{
            conn = Setup_Connection();
            channel = conn.CreateModel();
            byte[] messageBodyBytes = System.Text.Encoding.UTF8.GetBytes(sent_model._message);
            IBasicProperties props = channel.CreateBasicProperties();
                props.ContentType = "text/plain";
                props.DeliveryMode = 2;
                props.CorrelationId = correlation_id; 
            channel.BasicPublish("direct", sent_model._routing_key, props, messageBodyBytes);
        }
        catch(Exception e)
        {
            _logger.LogError(0, e, "Couldnt sent message to rabbitMQ", sent_model);
        }
        finally
        {
            channel.Close();
            conn.Close();
        } 
        //TODO: Wait for resonse. --> Return that value. --> 
        return respQueue.Take();  
    }

    /// <summary>
    /// Setup connection to RabbitMQ.
    /// </summary>
    /// <returns>IConnection object</returns>
    private IConnection Setup_Connection()
    {
        IConnection conn = null; 
        try{
            ConnectionFactory factory = new ConnectionFactory();
            factory.UserName = _configuration._user;
            factory.Password = _configuration._pass;
            factory.VirtualHost = _configuration._vhost;
            factory.HostName = _configuration._hostName;
            conn = factory.CreateConnection();
        }
        catch(Exception e)
        {
            _logger.LogError(0, e, "Couldnt connect to RabbitMQ server", conn); 
        }
        return conn; 
    }

    /// <summary>
    /// Only for test purpose. Code duplication. 
    /// </summary>
    /// <returns>Boolean</returns>
    public bool Test_Connection()
    {
        IConnection conn = null;
        IModel channel = null; 
        try
        {
            conn = Setup_Connection();
            channel = conn.CreateModel();
            byte[] messageBodyBytes = System.Text.Encoding.UTF8.GetBytes("TEST!");
            IBasicProperties props = channel.CreateBasicProperties();
                props.ContentType = "text/plain";
                props.DeliveryMode = 2;
            channel.BasicPublish("direct", "Search", props, messageBodyBytes);
            conn.Close(); 
            channel.Close(); 
        }
        catch(Exception)
        {
            Console.WriteLine("Couldnt sent message to RabbitMQ!");
            return false; 
        } 
        return true; 
    }
    }
}