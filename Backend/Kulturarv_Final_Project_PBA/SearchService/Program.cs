using System;
using RabbitMQ.Client; 

namespace SearchService
{
    #region Ressources
        //CQRS: https://www.eventstore.com/cqrs-pattern
        //RabbitMQ: https://www.rabbitmq.com/getstarted.html 
        //https://www.tutlane.com/tutorial/rabbitmq/csharp-read-messages-from-rabbitmq-queue
    #endregion
    public class Program
    {


        static void Main(string[] args)
        {
            //Search_Rpc search_Rpc = new Search_Rpc();
            Search_Service_IF searchService = new Search_Service();
            searchService.Search_By_Top_10_Close_Heritage_Sites(9.003, 52.0005);

        }


    } 
}
