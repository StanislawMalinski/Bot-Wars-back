using Communication.ServiceInterfaces;
using Shared.DataAccess.DTO;
using Shared.DataAccess.DataBaseEntities;
using Shared.Results;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Shared.DataAccess.RepositoryInterfaces;

public class PointsService : IPointsService
{
    private readonly IPointsRepository _pointsRepository;

    public PointsService(IPointsRepository pointsRepository)
    {
        _pointsRepository = pointsRepository;
    }

    public async Task<HandlerResult<Success, IErrorResult>> SetPointsForPlayer(long playerId, long points)
    {
        return await _pointsRepository.SetPointsForPlayer(playerId, points);
    } 
    
    public async Task<HandlerResult<SuccessData<List<PointHistoryDto>>, IErrorResult>> GetHistoryForPlayer(long playerId)
    {
        return await _pointsRepository.GetHistoryForPlayer(playerId);
    }

    public async Task<HandlerResult<SuccessData<List<Player>>,IErrorResult>> GetLeaderboards()
    {
        return await _pointsRepository.GetLeaderboards();
    }

    public async Task<HandlerResult<SuccessData<long>, IErrorResult>> GetPointsForPlayer(long playerId)
    {
        return await _pointsRepository.GetCurrentPointsForPlayer(playerId);
    }
}