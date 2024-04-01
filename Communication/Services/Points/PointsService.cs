using Communication.ServiceInterfaces;
using Shared.DataAccess.DTO;
using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.DTO.Responses;
using Shared.Results;
using Shared.Results.ErrorResults;
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
        return new IncorrectOperation();
    } 
    
    public async Task<HandlerResult<SuccessData<List<PointHistoryDto>>, IErrorResult>> GetHistoryForPlayer(long playerId)
    {
        return await _pointsRepository.GetHistoryForPlayer(playerId);
    }

    public async Task<HandlerResult<SuccessData<List<PlayerResponse>>,IErrorResult>> GetLeaderboards(int page, int pagesize)
    {
        return await _pointsRepository.GetLeaderboards(page, pagesize);
    }

    public async Task<HandlerResult<SuccessData<long>, IErrorResult>> GetPointsForPlayer(long playerId)
    {
        return await _pointsRepository.GetCurrentPointsForPlayer(playerId);
    }
    
}