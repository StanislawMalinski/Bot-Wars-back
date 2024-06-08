using Microsoft.EntityFrameworkCore.ChangeTracking;
using Shared.DataAccess.DataBaseEntities;

namespace Shared.DataAccess.RepositoryInterfaces;

public interface IUserSettingsRepository
{
    Task<UserSettings?> GetUserSetting(long playerId);
    Task<EntityEntry<UserSettings>> AddUserSetting(UserSettings userSettings);
    Task SaveChangesAsync();
}