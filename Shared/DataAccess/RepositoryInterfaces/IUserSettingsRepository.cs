using Shared.DataAccess.DAO;
using Shared.DataAccess.DataBaseEntities;
using Shared.Results;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Shared.DataAccess.RepositoryInterfaces;

public interface IUserSettingsRepository
{
    public Task<HandlerResult<Success,IErrorResult>> CreateUserSettingsForPlayer(long playerId);
    public Task<HandlerResult<SuccessData<UserSettingsDto>,IErrorResult>> GetUserSettingsForPlayer(long playerId);
    public Task<HandlerResult<Success,IErrorResult>> UpdateUserSettingsForPlayer(long playerId, UserSettingsDto dto);
}