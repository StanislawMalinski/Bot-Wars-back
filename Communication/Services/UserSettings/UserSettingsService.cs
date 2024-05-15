using Communication.ServiceInterfaces;
using Shared.DataAccess.DTO;
using Shared.DataAccess.Mappers;
using Shared.DataAccess.Repositories;
using Shared.DataAccess.RepositoryInterfaces;
using Shared.Results;
using Shared.Results.ErrorResults;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Communication.Services.UserSettings;

public class UserSettingsService : IUserSettingsService
{
    private readonly IUserSettingsRepository _userSettingsRepository;
    private readonly IPlayerRepository _playerRepository;
    private readonly IUserSettingsMapper _userSettingsMapper;

    public UserSettingsService(IUserSettingsRepository userSettingsRepository,IPlayerRepository playerRepository,IUserSettingsMapper userSettingsMapper)
    {
        _userSettingsRepository = userSettingsRepository;
        _playerRepository = playerRepository;
        _userSettingsMapper = userSettingsMapper;
    }
    
    public async Task<HandlerResult<Success, IErrorResult>> CreateUserSettingsForPlayer(long playerId)
    {
        var player = await _playerRepository.GetPlayer(playerId);
        if (player == null) return new EntityNotFoundErrorResult();
        

        if ((await _userSettingsRepository.GetUserSetting(playerId)) != null)
        {
            return new AccessDeniedError();
        }
        
        var userSettings = new Shared.DataAccess.DataBaseEntities.UserSettings
        {
            PlayerId = playerId,
            IsDarkTheme = false,
            Language = "English"
        };
        await _userSettingsRepository.AddUserSetting(userSettings);
        await _userSettingsRepository.SaveChangesAsync();
        return new Success();
    }

    public async Task<HandlerResult<SuccessData<UserSettingsDto>, IErrorResult>> GetUserSettingsForPlayer(long playerId)
    {
        var res = await _userSettingsRepository.GetUserSetting(playerId);
        if (res == null) return await CreateUserSettings(playerId);
        return new SuccessData<UserSettingsDto>()
        {
            Data = _userSettingsMapper.UserSettingsToDTO(res)
        };
    }

    public async Task<HandlerResult<Success, IErrorResult>> UpdateUserSettingsForPlayer(long playerId,
        UserSettingsDto dto)
    {
        var userSettings = await _userSettingsRepository.GetUserSetting(playerId);
        if (userSettings == null)
        {

            var settings = _userSettingsMapper.DtoToUserSettings(dto);
            settings.PlayerId = playerId;
            await _userSettingsRepository.AddUserSetting(settings);
            await _userSettingsRepository.SaveChangesAsync();
            return new Success();
        }
        userSettings.Language = dto.Language;
        userSettings.IsDarkTheme = dto.IsDarkTheme;
        await _userSettingsRepository.SaveChangesAsync();
        return new Success();
    }
    
    
    
    private async Task<HandlerResult<SuccessData<UserSettingsDto>, IErrorResult>> CreateUserSettings(long playerId)
    {
        var player = await _playerRepository.GetPlayer(playerId);
        if (player == null)
        {
            return new EntityNotFoundErrorResult()
            {
                Title = "EntityNotFoundErrorResult 404",
                Message = "No player with such id"
            };
        }

        if (await _userSettingsRepository.GetUserSetting(playerId) != null)
        {
            return new AccessDeniedError();
        }
        
        var userSettings = new Shared.DataAccess.DataBaseEntities.UserSettings
        {
            PlayerId = playerId,
            IsDarkTheme = false,
            Language = "English"
        };
        await _userSettingsRepository.AddUserSetting(userSettings);
        await _userSettingsRepository.SaveChangesAsync();
        return new SuccessData<UserSettingsDto>()
        {
            Data = _userSettingsMapper.UserSettingsToDTO(userSettings)
        };
    }
}