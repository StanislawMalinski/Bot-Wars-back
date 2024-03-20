using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace Shared.DataAccess.AuthorizationRequirements;

public class RoleNameToCreateAdminReqirementHandler : AuthorizationHandler<RoleNameToCreateAdminRequirement, int>
{
    
    private bool RoleMatch(string expectedRoleName, string? userRoleName, int requestedId)
    {
        if (requestedId == 1)
        {
            return true;
        }

        if (userRoleName is null)
        {
            return false;
        }
        return expectedRoleName.Equals(userRoleName);
    }

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleNameToCreateAdminRequirement requirement, int idRequestedToBeCreated)
    {
        var userRoleName = context.User.FindFirst(c => c.Type == ClaimTypes.Role)?.Value;
        
        var roleMatch = RoleMatch(requirement.RoleNameRequired, userRoleName, idRequestedToBeCreated);
        
        if (roleMatch)
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}