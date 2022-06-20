using System.Collections.Generic;

namespace Mobile_Gateway.rabbitmq 
{
public class RabbitMqConfiguration
{    
    public string _user {get; set;} 
    public string _pass {get; set;} 
    public string _vhost {get; set;} 
    public string _hostName {get; set;} 
    public List<string> _routing_keys {get; set;}
}
}