using Shared.DataAccess.DataBaseEntities;
using Shared.Results;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Shared.DataAccess.RepositoryInterfaces;

public interface IPointsService
{
    public Task<HandlerResult<SuccessData<PointHistory>,IErrorResult>> GetHistoryForPlayer(long playerId);
    public Task<HandlerResult<SuccessData<List<long>>, IErrorResult>> GetLeaderboards();
    public Task<HandlerResult<SuccessData<long>, IErrorResult>> GetPointsForPlayer(long playerId);
}