using Microsoft.AspNetCore.Authorization;

namespace Shared.DataAccess.AuthorizationRequirements;

public class RoleNameToCreateAdminRequirement : IAuthorizationRequirement
{
    public RoleNameToCreateAdminRequirement(string roleNameRequired)
    {
        RoleNameRequired = roleNameRequired;
    }

    public string RoleNameRequired { get; }
}