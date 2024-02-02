using Shared.DataAccess.DAO;
using Shared.DataAccess.DataBaseEntities;
using Shared.Results;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Shared.DataAccess.RepositoryInterfaces;

public interface IPointsRepository
{
    Task<HandlerResult<SuccessData<List<PointHistoryDto>>, IErrorResult>> GetHistoryForPlayer(long playerId);
    Task<HandlerResult<Success,IErrorResult>> SetPointsForPlayer(long playerId, long points);
    Task<HandlerResult<SuccessData<long>,IErrorResult>> GetCurrentPointsForPlayer(long playerId);
    Task<HandlerResult<SuccessData<List<Player>>,IErrorResult>> GetLeaderboards();
}