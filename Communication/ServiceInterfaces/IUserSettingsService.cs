using Shared.DataAccess.DTO;
using Shared.Results;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Communication.ServiceInterfaces;

public interface IUserSettingsService
{
    public Task<HandlerResult<Success, IErrorResult>> CreateUserSettingsForPlayer(long playerId);
    public Task<HandlerResult<SuccessData<UserSettingsDto>, IErrorResult>> GetUserSettingsForPlayer(long playerId);
    public Task<HandlerResult<Success, IErrorResult>> UpdateUserSettingsForPlayer(long playerId, UserSettingsDto dto);
}