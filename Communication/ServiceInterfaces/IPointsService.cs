using Shared.DataAccess.DTO;
using Shared.DataAccess.DTO.Responses;
using Shared.DataAccess.Pagination;
using Shared.Results;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Communication.ServiceInterfaces;

public interface IPointsService
{
    Task<HandlerResult<Success, IErrorResult>> SetPointsForPlayer(long playerId, long points);
    Task<HandlerResult<SuccessData<List<PointHistoryDto>>, IErrorResult>> GetHistoryForPlayer(long playerId);

    Task<HandlerResult<SuccessData<PageResponse<PlayerResponse>>, IErrorResult>> GetLeaderboards(
        PageParameters pageParameters);

    Task<HandlerResult<SuccessData<long>, IErrorResult>> GetPointsForPlayer(long playerId);
}