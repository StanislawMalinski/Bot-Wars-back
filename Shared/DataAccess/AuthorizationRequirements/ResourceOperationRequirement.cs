using Microsoft.AspNetCore.Authorization;

namespace Shared.DataAccess.AuthorizationRequirements;

public enum ResourceOperation
{
    Create,
    Read,
    Update,
    Delete,
    ReadRestricted
}

public class ResourceOperationRequirement : IAuthorizationRequirement
{
    public ResourceOperationRequirement(ResourceOperation resourceOperation)
    {
        ResourceOperation = resourceOperation;
    }

    public ResourceOperation ResourceOperation { get; }
}