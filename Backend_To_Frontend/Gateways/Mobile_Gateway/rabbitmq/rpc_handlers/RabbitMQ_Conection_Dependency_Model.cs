using System.Configuration; 
public class RabbitMQ_Conection_Dependency_Model
{
    //TODO: Get all the information about the connection.
    public string _url {get;} 
    public string _user {get;} 
    public string _pass {get;} 
    public string _vhost {get;} 
    public string _hostName {get;} 

    public RabbitMQ_Conection_Dependency_Model(string url, string user, string pass, string vhost, string hostName)
    {
        _url = url; 
        _user = user; 
        _pass = pass; 
        _vhost = vhost; 
        _hostName = hostName;  
    }
}