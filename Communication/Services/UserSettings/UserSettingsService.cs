using Communication.ServiceInterfaces;
using Communication.Services.Validation;
using Shared.DataAccess.DTO;
using Shared.Results;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Communication.Services.UserSettings;

public class UserSettingsService : Service<IUserSettingsService>
{
    private IUserSettingsService _userSettingsService;
    private string login = "login";
    private string key = "key";

    public UserSettingsService(UserSettingsAdminService adminInterface,
        UserSettingsIdentifiedPlayerService identifiedPlayerInterface,
        UserSettingsBannedPlayerService bannedPlayerInterface,
        UserSettingsUnidentifiedPlayerService unidentifiedUserInterface,
        UserSettingsBadValidation badValidationInterface,
        IPlayerValidator validator)
        : base(adminInterface, identifiedPlayerInterface, bannedPlayerInterface, unidentifiedUserInterface,
            badValidationInterface, validator)
    {
    }

    public async Task<HandlerResult<Success, IErrorResult>> CreateUserSettingsForPlayer(long playerId)
    {
        _userSettingsService = Validate(login, key);
        return await _userSettingsService.CreateUserSettingsForPlayer(playerId);
    }

    public async Task<HandlerResult<SuccessData<UserSettingsDto>, IErrorResult>> GetUserSettingsForPlayer(long playerId)
    {
        _userSettingsService = Validate(login, key);
        return await _userSettingsService.GetUserSettingsForPlayer(playerId);
    }

    public async Task<HandlerResult<Success, IErrorResult>> UpdateUserSettingsForPlayer(long playerId,
        UserSettingsDto dto)
    {
        _userSettingsService = Validate(login, key);
        return await _userSettingsService.UpdateUserSettingsForPlayer(playerId, dto);
    }
}