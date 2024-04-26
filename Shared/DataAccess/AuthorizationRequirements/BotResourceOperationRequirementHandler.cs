using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Shared.DataAccess.DataBaseEntities;

namespace Shared.DataAccess.AuthorizationRequirements;

public class BotResourceOperationRequirementHandler: AuthorizationHandler<ResourceOperationRequirement, Bot>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ResourceOperationRequirement requirement,
        Bot resource)
    {
        if (requirement.ResourceOperation == ResourceOperation.Read ||
            requirement.ResourceOperation == ResourceOperation.Create)
        {
            context.Succeed(requirement);
        }
        
        var userId = context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        var userRoleName = context.User.FindFirst(c => c.Type == ClaimTypes.Role)?.Value;

        if (userId is null || userRoleName is null)
        {
            context.Fail();
            return Task.CompletedTask;
        }
        
        if (resource.PlayerId == int.Parse(userId) || userRoleName.Equals("Admin"))
        {
            context.Succeed(requirement);
        }
        
        return Task.CompletedTask;
    }
}