using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Authentication 
{
    //Ressorce: https://auth0.com/docs/quickstart/backend/aspnet-core-webapi#configure-auth0-apis
    public class HasScopeHandler : AuthorizationHandler<Has_Scope_Requirements>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, Has_Scope_Requirements requirement)
    {
        // If user does not have the scope claim, get out of here
        if (!context.User.HasClaim(c => c.Type == "scope" && c.Issuer == requirement.Issuer))
            return Task.CompletedTask;

        // Split the scopes string into an array
        var scopes = context.User.FindFirst(c => c.Type == "scope" && c.Issuer == requirement.Issuer).Value.Split(' ');

        // Succeed if the scope array contains the required scope
        if (scopes.Any(s => s == requirement.Scope))
            context.Succeed(requirement);

        return Task.CompletedTask;
    }
}
}