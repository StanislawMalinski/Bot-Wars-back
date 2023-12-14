using Shared.Results;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Shared.DataAccess.RepositoryInterfaces;

public interface IAdministrativeService
{
    Task<HandlerResult<Success, IErrorResult>> UnbanPlayer(long playerId);
    Task<HandlerResult<Success, IErrorResult>> BanPlayer(long playerId);
}