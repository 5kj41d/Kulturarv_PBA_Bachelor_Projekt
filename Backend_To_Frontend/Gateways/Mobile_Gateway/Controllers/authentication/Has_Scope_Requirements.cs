using System;
using Microsoft.AspNetCore.Authorization;

namespace Authentication 
{
    //Ressource: https://auth0.com/docs/quickstart/backend/aspnet-core-webapi#configure-auth0-apis
    public class Has_Scope_Requirements : IAuthorizationRequirement
{
    public string Issuer { get; }
    public string Scope { get; }

    public Has_Scope_Requirements(string scope, string issuer)
    {
        Scope = scope ?? throw new ArgumentNullException(nameof(scope));
        Issuer = issuer ?? throw new ArgumentNullException(nameof(issuer));
    }
}
}