using System; 

namespace mobile_gateway_models 
{
    public class SentModel
    {
        public string _routing_key {get; private set;}
        public string _message {get; private set;}
        public SentModel(string routing_key, string message)
        {
            _routing_key = routing_key; 
            _message = message; 
        }
    }
}