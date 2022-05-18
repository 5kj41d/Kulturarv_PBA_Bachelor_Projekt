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
        static void Main(string[] args)
        {
            Search_Rpc search_Rpc = new Search_Rpc(); 
        }
    } 
}
