using System;
using RabbitMQ.Client;
using System.Text; 

namespace rabbitmq
{
    public interface RabbitMqRPCIF
    {
        
    }
    public class RabbitMqRPC : RabbitMqRPCIF
    {
        public RabbitMqRPC()
        {
            Init(); 
        }

        private void Init()
        {
            
        }
    }
}