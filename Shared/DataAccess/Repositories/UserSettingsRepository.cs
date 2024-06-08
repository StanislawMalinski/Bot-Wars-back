using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Shared.DataAccess.Context;
using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.RepositoryInterfaces;

namespace Shared.DataAccess.Repositories;

public class UserSettingsRepository : IUserSettingsRepository
{
    private readonly DataContext _dataContext;


    public UserSettingsRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<UserSettings?> GetUserSetting(long playerId)
    {
        return await _dataContext.UserSettings.FirstOrDefaultAsync(x => x.PlayerId == playerId);
    }

    public async Task SaveChangesAsync()
    {
        await _dataContext.SaveChangesAsync();
    }

    public async Task<EntityEntry<UserSettings>> AddUserSetting(UserSettings userSettings)
    {
        return await _dataContext.UserSettings.AddAsync(userSettings);
    }
}