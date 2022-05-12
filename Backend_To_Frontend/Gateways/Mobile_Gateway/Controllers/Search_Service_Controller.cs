using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Mobile_Gateway
{
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

    }
}