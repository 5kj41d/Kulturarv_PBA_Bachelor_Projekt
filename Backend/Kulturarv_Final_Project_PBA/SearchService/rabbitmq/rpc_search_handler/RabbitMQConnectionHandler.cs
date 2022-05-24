namespace rabbitmq.rpc_search_handler
{
    public class RabbitMQConnectionHandler
    {
        public string HostName {get; set;}
        public string Username {get; set;}
        public string Password { get; set; }
        public string VirtualHost { get; set; }
        public string Name { get; set; }
        public int port {get; set;}
        public string URL {get; set;}
    }
}