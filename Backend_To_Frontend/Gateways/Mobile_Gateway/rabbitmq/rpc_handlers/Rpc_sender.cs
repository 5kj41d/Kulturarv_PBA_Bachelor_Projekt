using System; 
using RabbitMQ.Client;

public class Rpc_sender : IDisposable
{
    //TODO: Send the message to the relevant service.
    private readonly string _url; 
    private readonly string _user; 
    private readonly string _pass; 
    private readonly string _vhost; 
    private readonly string _hostName; 
    private readonly string _uri; 
    private readonly IConnection _conn;
    public Rpc_sender(RabbitMQ_Conection_Dependency_Model model)
    {
    }

    /// <summary> 
    /// Version 1. Takes a string of search message to be sent. Returns void. 
    /// </summary>
    /// param name="message" of string
    public void Sent_Message_To_Message_Bus(string message)
    {
        IConnection conn = Setup_Connection();
    }
    
    private IConnection Setup_Connection()
    {
        ConnectionFactory factory = new ConnectionFactory();
        factory.UserName = _user;
        factory.Password = _pass;
        factory.VirtualHost = _vhost;
        factory.HostName = _hostName;
        factory.Uri = new Uri(_uri); 
        IConnection conn = factory.CreateConnection();
        return conn; 
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }
}