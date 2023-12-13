using Shared.DataAccess.DataBaseEntities;
using Shared.Results;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Shared.DataAccess.RepositoryInterfaces;

public interface IPointHistoryRepository
{
    public Task<HandlerResult<SuccessData<List<PointHistory>>,IErrorResult>> GetHistoryForPlayer(long playerId);

}