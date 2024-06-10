using Communication.ServiceInterfaces;
using Shared.DataAccess.DTO;
using Shared.DataAccess.DTO.Responses;
using Shared.DataAccess.Pagination;
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

    public async Task<HandlerResult<SuccessData<List<PointHistoryDto>>, IErrorResult>> GetHistoryForPlayer(
        long playerId)
    {
        return new SuccessData<List<PointHistoryDto>>
        {
            Data = await _pointsRepository.GetHistoryForPlayer(playerId)
        };
    }

    public async Task<HandlerResult<SuccessData<PageResponse<PlayerResponse>>, IErrorResult>> GetLeaderboards(
        PageParameters pageParameters)
    {
        return new SuccessData<PageResponse<PlayerResponse>>
        {
            Data = new PageResponse<PlayerResponse>(await _pointsRepository.GetLeaderboards(pageParameters),
                pageParameters.PageSize, await _pointsRepository.NumberOfLeaderBoard())
        };
    }

    public async Task<HandlerResult<SuccessData<long>, IErrorResult>> GetPointsForPlayer(long playerId)
    {
        return new SuccessData<long>
        {
            Data = await _pointsRepository.GetCurrentPointsForPlayer(playerId)
        };
    }
}