using Shared.DataAccess.DAO;
using Shared.DataAccess.Repositories;
using Shared.Results;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Communication.Services.UserSettings;

public class UserSettingsServiceProvider
{
    private readonly UserSettingsRepository _userSettingsRepository;

    public UserSettingsServiceProvider(UserSettingsRepository userSettingsRepository)
    {
        _userSettingsRepository = userSettingsRepository;
    }
    
    public async Task<HandlerResult<Success, IErrorResult>> CreateUserSettingsForPlayer(long playerId)
    {
        return await _userSettingsRepository.CreateUserSettingsForPlayer(playerId);
    }

    public async Task<HandlerResult<SuccessData<UserSettingsDto>, IErrorResult>> GetUserSettingsForPlayer(long playerId)
    {
        return await _userSettingsRepository.GetUserSettingsForPlayer(playerId);
    }

    public async Task<HandlerResult<Success, IErrorResult>> UpdateUserSettingsForPlayer(long playerId,
        UserSettingsDto dto)
    {
        return await _userSettingsRepository.UpdateUserSettingsForPlayer(playerId, dto);
    }
}