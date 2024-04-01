using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Shared.DataAccess.Context;
using Shared.DataAccess.DTO;
using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.DTO.Responses;
using Shared.DataAccess.Mappers;
using Shared.DataAccess.MappersInterfaces;
using Shared.DataAccess.RepositoryInterfaces;
using Shared.Results;
using Shared.Results.ErrorResults;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Shared.DataAccess.Repositories;

public class PointRepository : IPointsRepository
{
    private readonly DataContext _dataContext;
    private readonly IPointHistoryMapper _pointHistoryMapper;
    private readonly IPlayerMapper _playerMapper;

    public PointRepository(DataContext dataContext, IPointHistoryMapper pointHistoryMapper,IPlayerMapper playerMapper)
    {
        _dataContext = dataContext;
        _playerMapper = playerMapper;
        _pointHistoryMapper = pointHistoryMapper;
    }

    public async Task<HandlerResult<SuccessData<List<PointHistoryDto>>, IErrorResult>> GetHistoryForPlayer(long playerId)
    {
        var player = await _dataContext.Players.FindAsync(playerId);
        if (player == null)
        {
            return new EntityNotFoundErrorResult()
            {
                Title = "EntityNotFoundErrorResult 404",
                Message = "No player with such id"
            };
        }

        var pointHistory = await _dataContext.PointHistories
            .Where(pointsHistory => pointsHistory.PlayerId == playerId)
            .ToListAsync();
        
        return new SuccessData<List<PointHistoryDto>>
        {
            Data = pointHistory.ConvertAll(element => _pointHistoryMapper
                .MapPointHistoryToPointHistoryDto(element))
        };

    }

    public async Task<HandlerResult<SuccessData<long>, IErrorResult>> GetCurrentPointsForPlayer(long playerId)
    {
        var player = await _dataContext.Players.FindAsync(playerId);
        if (player == null)
        {
            return new EntityNotFoundErrorResult()
            {
                Title = "EntityNotFoundErrorResult 404",
                Message = "No player with such id"
            };
        }

        return new SuccessData<long>()
        {
            Data = player.Points
        };
    }

    public async Task<HandlerResult<SuccessData<List<PlayerResponse>>, IErrorResult>> GetLeaderboards(int page, int pagesize)
    {
        var players = await _dataContext.Players
            .OrderByDescending(player => player.Points).Select(x=> _playerMapper.ToPlayerResponse(x))
            .Skip(page * pagesize)
            .Take(pagesize)
            .ToListAsync();

        return new SuccessData<List<PlayerResponse>>
        {
            Data = players
        };
    }

    public async Task<HandlerResult<SuccessData<long>, IErrorResult>> GetPlayerPoint(long playerId)
    {
        var res = await _dataContext.Players.FindAsync(playerId);
        if (res == null) return new EntityNotFoundErrorResult();
        return new SuccessData<long>()
        {
            Data = res.Points
        };
    }

    public async Task<HandlerResult<Success, IErrorResult>> UpdatePointsForPlayerNoSave(long playerId, long points,long tourId)
    {
        var res = await _dataContext.Players.FindAsync(playerId);
        if (res == null) return new EntityNotFoundErrorResult();
       
        var pointHistory = new PointHistory()
        {
            PlayerId = playerId,
            Before = res.Points,
            Change = points,
            TournamentId = tourId,
            LogDate = DateTime.Now
        };
        res.Points += points;
        _dataContext.Players.Update(res);
        await _dataContext.PointHistories.AddAsync(pointHistory);
        
        return new Success();
    }
}