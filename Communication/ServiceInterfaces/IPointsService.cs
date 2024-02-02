using Shared.DataAccess.DAO;
using Shared.DataAccess.DataBaseEntities;
using Shared.Results;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Communication.ServiceInterfaces;

public interface IPointsService
{
    Task<HandlerResult<Success, IErrorResult>> SetPointsForPlayer(long playerId, long points);
    Task<HandlerResult<SuccessData<List<PointHistoryDto>>,IErrorResult>> GetHistoryForPlayer(long playerId);
    Task<HandlerResult<SuccessData<List<Player>>, IErrorResult>> GetLeaderboards();
    Task<HandlerResult<SuccessData<long>, IErrorResult>> GetPointsForPlayer(long playerId);
}