using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Shared.DataAccess.Context;
using Shared.DataAccess.DTO;
using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.Mappers;
using Shared.DataAccess.RepositoryInterfaces;
using Shared.Results;
using Shared.Results.ErrorResults;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

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