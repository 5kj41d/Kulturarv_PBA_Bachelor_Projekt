using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Mobile_Gateway
{
    //Location, time age, heritage type, region, tilkoblet events. 
    //Ressources: https://manage.auth0.com/dashboard/eu/dev-k24ialwx/applications/AzRrQU4RT74bFrFFnwzNa3UM6W3PEVJP/quickstart 
    //https://docs.microsoft.com/en-us/dotnet/architecture/microservices/architect-microservice-container-applications/direct-client-to-microservice-communication-versus-the-api-gateway-pattern
    [ApiController]
    [Route("[controller]")]
    [ApiVersion("1.0")]
    public class Search_Service_Controller : ControllerBase
    {
        private readonly ILogger<Search_Service_Controller> _logger;
        private readonly IOptions<RabbitMqConfiguration> _configuration; 
        public Search_Service_Controller(ILogger<Search_Service_Controller> logger, IOptions<RabbitMqConfiguration> options)
        {
            _logger = logger; 
            _configuration = options;
        } 

        [Route("api/Search_Service_Controller/")]
        [HttpGet]
        [Authorize]
        [ApiVersion("1.0")]
        public IEnumerable<string> Get()
        {
            string request_routing_key = "Search_All"; 
            string request_message = "Get all"; 
            Rpc_sender sender = new Rpc_sender(_configuration, _logger);  //TODO: Fix this. 
            
            //TODO: Check for JWTs.
            //TODO: Error check if something goes wrong. 
            Sent_Model sent_Model = new Sent_Model
            {
                message = request_message,
                routing_key = request_routing_key
            };
            return sender.Sent_Message_To_Message_Bus_RPC(sent_Model);
        
        }

        [Route("api/Search_Service_Controller/{type}")]
        [HttpGet]
        [Authorize]
        [ApiVersion("1.0")]
        public IEnumerable<string> Get_Heritage_Type(string type)
        {
            return null; 
        }

        [Route("api/Search_Service_Controller/{timeage}")]
        [HttpGet]
        [Authorize]
        [ApiVersion("1.0")]
        public IEnumerable<string> Get_All_From_Time_Age(int age)
        {
            return null; 
        }

        [Route("api/Search_Service_Controller/{region}")]
        [HttpGet]
        [Authorize]
        [ApiVersion("1.0")]
        public IEnumerable<string> Get_All_From_Region(string region)
        {
            return null; 
        }

        //TODO: Get all from associated events.
        //Method goes here --> 

    }
}