using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

public interface Rpc_sender_IF 
{
    public IEnumerable<string> Sent_Message_To_Message_Bus_RPC(Sent_Model sent_model);
}

public class Rpc_sender : Rpc_sender_IF
{
    //TODO: Send the message to the relevant service.
    private readonly ILogger _logger; 
    private readonly RabbitMqConfiguration _configuration; 
    public Rpc_sender(IOptions<RabbitMqConfiguration> options, ILogger logger)
    {
        _configuration = options.Value; 
        _logger = logger; 
    }

    /// <summary> 
    /// Version 1. Takes a string of search message to be sent. Returns void. 
    /// </summary>
    /// param name="message" of string
    public IEnumerable<string> Sent_Message_To_Message_Bus_RPC(Sent_Model sent_model)
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
        return null;  
    }

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
    /// Only for test purpose.
    /// </summary>
    /// <returns>bool</returns>
    public bool Test_Connection()
    {
        //TODO: Finish!
        return true; 
    }
}