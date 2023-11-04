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
    [ApiVersion("1.0")]
    public class Authentication_Controller 
    {
        private readonly ILogger _logger; 

        public Authentication_Controller(ILogger logger)
        {
            _logger = logger; 
        }

        ////TODO: When needed, finish the authentication part. 
        ////TODO: Should have a microservice to handle this instead of using http requests to the auth0 server. --> Destroys the purpose of asynchronous design. 
        //[Route("")]
        //[HttpPost]
        //[ApiVersion("1.0")]
        //public async Task Login()
        //{
        //    var authenticationProperties = new LoginAuthenticationPropertiesBuilder().Build(); 
        //    //await HttpContext.ChallengeAsync(Auth0Constants.AuthenticationScheme, authenticationProperties); 
        //}

        //[Route("")]
        //[HttpPost]
        //[Authorize]
        //[ApiVersion("1.0")]
        //public async void Logout()
        //{
        //    var authenticationProperties = new LogoutAuthenticationPropertiesBuilder().Build();
        //    //-->await HttpContext.SignOutAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
        //    //await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme); 
        //}
    }
}