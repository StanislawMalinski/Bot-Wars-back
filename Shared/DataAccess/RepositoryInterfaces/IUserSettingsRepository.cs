using Microsoft.EntityFrameworkCore.ChangeTracking;
using Shared.DataAccess.DTO;
using Shared.DataAccess.DataBaseEntities;
using Shared.Results;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Shared.DataAccess.RepositoryInterfaces;

public interface IUserSettingsRepository
{
    Task<UserSettings?> GetUserSetting(long playerId);
    Task<EntityEntry<UserSettings>> AddUserSetting(UserSettings userSettings);
    Task SaveChangesAsync();
   
}