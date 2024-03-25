using Microsoft.AspNetCore.Authorization;

namespace Shared.DataAccess.AuthorizationRequirements;

public class RoleNameToCreateAdminRequirement : IAuthorizationRequirement
{

    public string RoleNameRequired { get; }

    public RoleNameToCreateAdminRequirement(string roleNameRequired)
    {
        RoleNameRequired = roleNameRequired;
    }

}
    
