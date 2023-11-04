using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client; 
using rabbitmq.rpc_search_handler;



namespace SearchService
{
    #region Ressources
        //CQRS: https://www.eventstore.com/cqrs-pattern
        //RabbitMQ: https://www.rabbitmq.com/getstarted.html 
        //https://www.tutlane.com/tutorial/rabbitmq/csharp-read-messages-from-rabbitmq-queue
        //https://raw.githubusercontent.com/dotnet-architecture/eBooks/main/current/microservices/NET-Microservices-Architecture-for-Containerized-NET-Applications.pdf
    #endregion
    public class Program
    {
        static async Task Main(string[] args)
        {

            IConfiguration Config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.Development.json")
                .SetBasePath("C:\\Users\\fanes\\Source\\Repos\\Sk41d\\Kulturarv_PBA_Bachelor_Projekt\\Backend\\Kulturarv_Final_Project_PBA\\SearchService")
                .Build();
            Search_Rpc rpc = new  Search_Rpc(Config); 
            //TODO: Should run all the time. 
            //Search_Rpc search_Rpc = new Search_Rpc();
            Search_Service_IF searchService = new Search_Service();
            //await searchService.Search_By_Top_10_Close_Heritage_Sites(9.003, 52.0005);

        }


    } 
}
