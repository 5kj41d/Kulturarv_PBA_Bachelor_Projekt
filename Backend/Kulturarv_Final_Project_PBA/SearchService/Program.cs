using System;
using RabbitMQ.Client; 

namespace SearchService
{
    #region Ressources
        //CQRS: https://www.eventstore.com/cqrs-pattern
        //RabbitMQ: https://www.rabbitmq.com/getstarted.html 
        //https://www.tutlane.com/tutorial/rabbitmq/csharp-read-messages-from-rabbitmq-queue
    #endregion
    class Program
    {
        private const string UserName = "guest";
        private const string Password = "guest";
        private const string HostName = "localhost";
        static void Main(string[] args)
        {
            ConnectionFactory connectionFactory = new ConnectionFactory
            {
                HostName = HostName,
                UserName = UserName,
                Password = Password,
            };
            var connection = connectionFactory.CreateConnection();
            var channel = connection.CreateModel();
            // accept only one unack-ed message at a time
            // uint prefetchSize, ushort prefetchCount, bool global

            channel.BasicQos(0, 1, false);
            Search_Consumer messageReceiver = new Search_Consumer(channel);
            channel.BasicConsume("demoqueue", false, messageReceiver);
            Console.ReadLine();
        }
    }
        //Log: Id, Beskedinhold, bruger, tidspunktet, Log_årsag. 
}
