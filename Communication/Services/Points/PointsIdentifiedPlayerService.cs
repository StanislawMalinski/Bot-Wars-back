using Communication.ServiceInterfaces;
using Shared.DataAccess.DAO;
using Shared.DataAccess.DataBaseEntities;
using Shared.Results;
using Shared.Results.ErrorResults;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Shared.DataAccess.RepositoryInterfaces;

public class PointsIdentifiedPlayerService : IPointsService
{
    protected readonly PointsServiceProvider _pointsServiceProvider;

    public PointsIdentifiedPlayerService(PointsServiceProvider pointsServiceProvider)
    {
        _pointsServiceProvider = pointsServiceProvider;
    }

    public virtual async Task<HandlerResult<Success, IErrorResult>> SetPointsForPlayer(long playerId, long points)
    {
        return new AccessDeniedError();
    }

    public async Task<HandlerResult<SuccessData<List<PointHistoryDto>>, IErrorResult>> GetHistoryForPlayer(long playerId)
    {
        return await _pointsServiceProvider.GetHistoryForPlayer(playerId);
    }

    public async Task<HandlerResult<SuccessData<List<Player>>, IErrorResult>> GetLeaderboards()
    {
        return await _pointsServiceProvider.GetLeaderboards();
    }

    public async Task<HandlerResult<SuccessData<long>, IErrorResult>> GetPointsForPlayer(long playerId)
    {
        return await _pointsServiceProvider.GetPointsForPlayer(playerId);
    }
}