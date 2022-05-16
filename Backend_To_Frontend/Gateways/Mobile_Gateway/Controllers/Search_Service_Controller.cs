using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Mobile_Gateway
{
    //Location, time age, heritage type, region, tilkoblet events. 

    //https://docs.microsoft.com/en-us/dotnet/architecture/microservices/architect-microservice-container-applications/direct-client-to-microservice-communication-versus-the-api-gateway-pattern
    [ApiController]
    [Route("[controller]")]
    [ApiVersion("1.0")]
    public class Search_Service_Controller : ControllerBase
    {
        private readonly ILogger<Search_Service_Controller> _logger;
        public Search_Service_Controller(ILogger<Search_Service_Controller> logger)
        {
            _logger = logger; 
        } 

        [Route("api/Search_Service_Controller/")]
        [HttpGet]
        [Authorize]
        public IEnumerable<string> Get()
        {
            string routing_key = "Search_All"; 
            string message = "Get all"; 
            Rpc_sender sender = new Rpc_sender(null, _logger);  //TODO: Fix this. 
            
            //TODO: Check for JWTs.
            
            return null;  
        }

        [Route("api/Search_Service_Controller/{type}")]
        [HttpGet]
        [Authorize]
        public IEnumerable<string> Get_Heritage_Type(string type)
        {
            return null; 
        }

        [Route("api/Search_Service_Controller/{timeage}")]
        [HttpGet]
        [Authorize]
        public IEnumerable<string> Get_All_From_Time_Age(int age)
        {
            return null; 
        }

        [Route("api/Search_Service_Controller/{region}")]
        [HttpGet]
        [Authorize]
        public IEnumerable<string> Get_All_From_Region(string region)
        {
            return null; 
        }

        //TODO: Get all from associated events.
        //Method goes here --> 

    }
}