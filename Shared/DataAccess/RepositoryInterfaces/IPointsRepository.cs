using Shared.DataAccess.DTO;
using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.DTO.Responses;
using Shared.Results;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Shared.DataAccess.RepositoryInterfaces;

public interface IPointsRepository
{
    Task<HandlerResult<SuccessData<List<PointHistoryDto>>, IErrorResult>> GetHistoryForPlayer(long playerId);
    Task<HandlerResult<SuccessData<long>,IErrorResult>> GetCurrentPointsForPlayer(long playerId);
    Task<HandlerResult<SuccessData<List<PlayerResponse>>,IErrorResult>> GetLeaderboards(int page, int pagesize);
    Task<HandlerResult<SuccessData<long>, IErrorResult>> GetPlayerPoint(long playerId);
    Task<HandlerResult<Success, IErrorResult>> UpdatePointsForPlayerNoSave(long playerId, long points,long tourId);
}