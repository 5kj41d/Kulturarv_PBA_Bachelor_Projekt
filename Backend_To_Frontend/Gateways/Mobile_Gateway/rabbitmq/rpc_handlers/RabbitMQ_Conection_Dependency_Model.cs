using System.Configuration; 
public class RabbitMQ_Conection_Dependency_Model
{
    //TODO: Get all the information about the connection.
    public string _url {private set; get;} 
    public string _user {private set; get;} 
    public string _pass {private set; get;} 
    public string _vhost {private set; get;} 
    public string _hostName {private set; get;} 
    public string _uri {private set; get;} 

    public RabbitMQ_Conection_Dependency_Model()
    {
        //TODO: Configuration file is needed with connection information. 
    }
}