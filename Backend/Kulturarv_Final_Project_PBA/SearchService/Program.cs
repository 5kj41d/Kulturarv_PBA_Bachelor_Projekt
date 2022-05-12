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
            Search_Gatway_Request_Consumer _Consumer = new Search_Gatway_Request_Consumer(); 
        }
    } 
}
