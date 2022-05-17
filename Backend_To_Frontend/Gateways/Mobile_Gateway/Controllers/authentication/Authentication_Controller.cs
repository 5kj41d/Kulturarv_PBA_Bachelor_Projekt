using System;
using System.Threading.Tasks;
using Auth0.AspNetCore.Authentication;
//using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        public async Task Login()
        {
            var authenticationProperties = new LoginAuthenticationPropertiesBuilder().Build(); 
            //await HttpContext.ChallengeAsync(Auth0Constants.AuthenticationScheme, authenticationProperties); 
        }

        [Route("")]
        [HttpPost]
        [Authorize]
        public async void Logout()
        {
            var authenticationProperties = new LogoutAuthenticationPropertiesBuilder().Build();
            //-->await HttpContext.SignOutAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
            //await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme); 
        }
    }
}