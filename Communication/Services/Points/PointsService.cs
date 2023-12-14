using Shared.DataAccess.DataBaseEntities;
using Shared.Results;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Shared.DataAccess.RepositoryInterfaces;

public class PointsService : IPointsService
{
    public Task<HandlerResult<SuccessData<PointHistory>, IErrorResult>> GetHistoryForPlayer(long playerId)
    {
        throw new NotImplementedException();
    }

    public Task<HandlerResult<SuccessData<List<long>>, IErrorResult>> GetLeaderboards()
    {
        throw new NotImplementedException();
    }

    public Task<HandlerResult<SuccessData<long>, IErrorResult>> GetPointsForPlayer(long playerId)
    {
        throw new NotImplementedException();
    }
}