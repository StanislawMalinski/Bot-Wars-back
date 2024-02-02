using Communication.ServiceInterfaces;
using Shared.DataAccess.DAO;
using Shared.Results;
using Shared.Results.ErrorResults;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Communication.Services.UserSettings;

public class UserSettingsBadValidation : IUserSettingsService
{
    public async Task<HandlerResult<Success, IErrorResult>> CreateUserSettingsForPlayer(long playerId)
    {
        return new AccessDeniedError();
    }

    public async Task<HandlerResult<SuccessData<UserSettingsDto>, IErrorResult>> GetUserSettingsForPlayer(long playerId)
    {
        return new AccessDeniedError();
    }

    public async Task<HandlerResult<Success, IErrorResult>> UpdateUserSettingsForPlayer(long playerId, UserSettingsDto dto)
    {
        return new AccessDeniedError();
    }
}