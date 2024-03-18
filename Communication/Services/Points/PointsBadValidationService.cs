using Communication.ServiceInterfaces;
using Shared.DataAccess.DTO;
using Shared.DataAccess.DataBaseEntities;
using Shared.Results;
using Shared.Results.ErrorResults;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Shared.DataAccess.RepositoryInterfaces;

public class PointsBadValidationService : IPointsService
{
    public async Task<HandlerResult<Success, IErrorResult>> SetPointsForPlayer(long playerId, long points)
    {
        return new AccessDeniedError();
    }
    

    public async Task<HandlerResult<SuccessData<List<PointHistoryDto>>, IErrorResult>> GetHistoryForPlayer(long playerId)
    {
        return new AccessDeniedError();
    }

    public async Task<HandlerResult<SuccessData<List<Player>>, IErrorResult>> GetLeaderboards()
    {
        return new AccessDeniedError();
    }

    public async Task<HandlerResult<SuccessData<long>, IErrorResult>> GetPointsForPlayer(long playerId)
    {
        return new AccessDeniedError();
    }
}