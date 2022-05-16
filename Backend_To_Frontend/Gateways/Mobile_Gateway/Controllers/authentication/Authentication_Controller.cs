using System; 
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Controllers
{
    public class Authentication_Controller 
    {
        private readonly ILogger _logger; 

        public Authentication_Controller(ILogger logger)
        {
            _logger = logger; 
        }

        [Route("")]
        [HttpPost]
        public void Login()
        {
            //TODO: Sent message to auth service message bus. 
        }

        [Route("")]
        [HttpPost]
        public void Logout()
        {

        }
    }
}