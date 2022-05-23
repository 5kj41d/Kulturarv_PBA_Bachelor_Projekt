using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Mobile_Gateway.rabbitmq;

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
        //TODO: Make the routing key access more clean. And the message part of the sent objects. 
        private readonly ILogger<Search_Service_Controller> _logger;
        private readonly IOptions<RabbitMqConfiguration> _configuration; 
        public Search_Service_Controller(ILogger<Search_Service_Controller> logger, IOptions<RabbitMqConfiguration> options)
        {
            _logger = logger; 
            _configuration = options;
        } 

        #region Version 1 API surface methods.

        [Route("api/Search_Service_Controller/")]
        [HttpGet]
        //[Authorize]
        [ApiVersion("1.0")]
        public string Get()
        {
            Rpc_sender_IF sender = new Rpc_sender(_configuration, _logger);  //TODO: Fix this. 
            
            //TODO: Check for JWTs.
            //TODO: Error check if something goes wrong. 

            Sent_Model sent_Model = new Sent_Model(_configuration.Value._routing_keys[0], "Get_All");
            return sender.Sent_Message_To_Message_Bus_RPC(sent_Model);
        }

        [Route("api/Search_Service_Controller/{type}")]
        [HttpGet]
        //[Authorize]
        [ApiVersion("1.0")]
        public string Get_Heritage_Type(string type)
        {
            Rpc_sender_IF sender = new Rpc_sender(_configuration, _logger);
            //TODO: Check for JWTs.
            //TODO: Error check if something goes wrong.
            Sent_Model sent_Model = new Sent_Model(_configuration.Value._routing_keys[0], type);
            return sender.Sent_Message_To_Message_Bus_RPC(sent_Model); 
        }

        [Route("api/Search_Service_Controller/{timeage}")]
        [HttpGet]
        //[Authorize]
        [ApiVersion("1.0")]
        public string Get_All_From_Time_Age(int age)
        {
            Rpc_sender_IF sender = new Rpc_sender(_configuration, _logger);
            //TODO: Check for JWTs.
            //TODO: Error check if something goes wrong.
            Sent_Model sent_Model = new Sent_Model(_configuration.Value._routing_keys[0], age.ToString());
            return sender.Sent_Message_To_Message_Bus_RPC(sent_Model); 
        }

        [Route("api/Search_Service_Controller/{region}")]
        [HttpGet]
        //[Authorize]
        [ApiVersion("1.0")]
        public string Get_All_From_Region(string region)
        {
            Rpc_sender_IF sender = new Rpc_sender(_configuration, _logger);
            //TODO: Check for JWTs.
            //TODO: Error check if something goes wrong.
            Sent_Model sent_Model = new Sent_Model(_configuration.Value._routing_keys[0], region);
            return sender.Sent_Message_To_Message_Bus_RPC(sent_Model);  
        }

        //TODO: Get all from associated events.
        //Method goes here --> 
        #endregion

    }
}