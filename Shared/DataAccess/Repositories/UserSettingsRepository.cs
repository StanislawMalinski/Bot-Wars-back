using System.Data;
using Microsoft.EntityFrameworkCore;
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
    private readonly IUserSettingsMapper _userSettingsMapper;

    public UserSettingsRepository(DataContext dataContext, IUserSettingsMapper userSettingsMapper)
    {
        _dataContext = dataContext;
        _userSettingsMapper = userSettingsMapper;
    }

    public async Task<HandlerResult<Success, IErrorResult>> CreateUserSettingsForPlayer(long playerId)
    {
        var player = await _dataContext.Players.FindAsync(playerId);
        if (player == null)
        {
            return new EntityNotFoundErrorResult()
            {
                Title = "EntityNotFoundErrorResult 404",
                Message = "No player with such id"
            };
        }

        if (await _dataContext.UserSettings.FirstOrDefaultAsync(userSettings => 
                userSettings.PlayerId==playerId) != null)
        {
            return new AccessDeniedError();
        }
        
        var userSettings = new UserSettings
        {
            PlayerId = playerId,
            IsDarkTheme = false,
            Language = "English"
        };
        await _dataContext.UserSettings.AddAsync(userSettings);
        await _dataContext.SaveChangesAsync();
        return new Success();
    }
    private async Task<HandlerResult<SuccessData<UserSettingsDto>, IErrorResult>> CreateUserSettings(long playerId)
    {
        var player = await _dataContext.Players.FindAsync(playerId);
        if (player == null)
        {
            return new EntityNotFoundErrorResult()
            {
                Title = "EntityNotFoundErrorResult 404",
                Message = "No player with such id"
            };
        }

        if (await _dataContext.UserSettings.FirstOrDefaultAsync(userSettings => 
                userSettings.PlayerId==playerId) != null)
        {
            return new AccessDeniedError();
        }
        
        var userSettings = new UserSettings
        {
            PlayerId = playerId,
            IsDarkTheme = false,
            Language = "English"
        };
        await _dataContext.UserSettings.AddAsync(userSettings);
        await _dataContext.SaveChangesAsync();
        return new SuccessData<UserSettingsDto>()
        {
            Data = _userSettingsMapper.UserSettingsToDTO(userSettings)
        };
    }
    public async Task<HandlerResult<SuccessData<UserSettingsDto>, IErrorResult>> GetUserSettingsForPlayer(long playerId)
    {
        var userSettings = await _dataContext.UserSettings.FirstOrDefaultAsync(userSettings =>
            userSettings.PlayerId==playerId);
        if (userSettings == null)
        {
            return await CreateUserSettings(playerId);
        }

        return new SuccessData<UserSettingsDto>()
        {
            Data = _userSettingsMapper.UserSettingsToDTO(userSettings)
        };
    }

    public async Task<HandlerResult<Success, IErrorResult>> UpdateUserSettingsForPlayer(long playerId,
        UserSettingsDto dto)
    {
        var userSettings = await _dataContext.UserSettings.FirstOrDefaultAsync(x=>x.PlayerId == playerId);
        if (userSettings == null)
        {

            var settings = _userSettingsMapper.DtoToUserSettings(dto);
            settings.PlayerId = playerId;
            await _dataContext.UserSettings.AddAsync(settings);
            await _dataContext.SaveChangesAsync();
            return new Success();
        }
        userSettings.Language = dto.Language;
        userSettings.IsDarkTheme = dto.IsDarkTheme;
        await _dataContext.SaveChangesAsync();
        return new Success();
    }
}