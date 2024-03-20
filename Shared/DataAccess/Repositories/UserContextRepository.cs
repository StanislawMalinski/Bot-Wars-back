using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Shared.DataAccess.RepositoryInterfaces;

namespace Shared.DataAccess.Repositories;

public class UserContextRepository: IUserContextRepository
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserContextRepository(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public ClaimsPrincipal? GetUser()
    {
        var accessor = _httpContextAccessor.HttpContext?.User;
        return accessor;
    }

    public long? GetUserId()
    {
        var user = GetUser();
        if (user == null)
        {
            return null;
        }
        if (long.TryParse(user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value, out long userId))
        {
            return userId;
        }
        else
        {
            return null;
        }
    }

    public string? GetUserRole()
    {
        var user = GetUser();
        if (user == null)
        {
            return null;
        };
        var role = user.FindFirst(c => c.Type == ClaimTypes.Role);
        if (role is not null)
        {
            return role.Value;
        }
        else
        {
            return null;
        }
    }
}  