using System.Security.Claims;

namespace Shared.DataAccess.RepositoryInterfaces;

public interface IUserContextRepository
{
    public ClaimsPrincipal? GetUser();
    public long? GetUserId();
    public string? GetUserRole();
}