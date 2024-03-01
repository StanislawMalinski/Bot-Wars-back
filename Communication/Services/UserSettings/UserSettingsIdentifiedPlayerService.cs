using Communication.ServiceInterfaces;
using Shared.DataAccess.DTO;
using Shared.Results;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Communication.Services.UserSettings;

public class UserSettingsIdentifiedPlayerService : IUserSettingsService
{
    private readonly UserSettingsServiceProvider _userSettingsServiceProvider;

    public UserSettingsIdentifiedPlayerService(UserSettingsServiceProvider userSettingsServiceProvider)
    {
        _userSettingsServiceProvider = userSettingsServiceProvider;
    }

    public async Task<HandlerResult<Success, IErrorResult>> CreateUserSettingsForPlayer(long playerId)
    {
        return await _userSettingsServiceProvider.CreateUserSettingsForPlayer(playerId);
    }

    public async Task<HandlerResult<SuccessData<UserSettingsDto>, IErrorResult>> GetUserSettingsForPlayer(long playerId)
    {
        return await _userSettingsServiceProvider.GetUserSettingsForPlayer(playerId);
    }

    public async Task<HandlerResult<Success, IErrorResult>> UpdateUserSettingsForPlayer(long playerId,
        UserSettingsDto dto)
    {
        return await _userSettingsServiceProvider.UpdateUserSettingsForPlayer(playerId, dto);
    }
}