using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Shared.DataAccess.Context;
using Shared.DataAccess.DTO;
using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.DTO.Responses;
using Shared.DataAccess.Mappers;
using Shared.DataAccess.MappersInterfaces;
using Shared.DataAccess.Pagination;
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

    public async Task<List<PointHistoryDto>> GetHistoryForPlayer(long playerId)
    {
        var player = await _dataContext.Players.FindAsync(playerId);
        if (player == null) return new List<PointHistoryDto>();

        var pointHistory = await _dataContext.PointHistories
            .Where(pointsHistory => pointsHistory.PlayerId == playerId).OrderBy(x=>x.LogDate)
            .ToListAsync();
        
        return pointHistory.ConvertAll(element => _pointHistoryMapper
                .MapPointHistoryToPointHistoryDto(element));

    }

    public async Task<long> GetCurrentPointsForPlayer(long playerId)
    {
        var player = await _dataContext.Players.FindAsync(playerId);
        if (player == null) return 0;

        return player.Points;
    }

    public async Task<List<PlayerResponse>> GetLeaderboards(PageParameters pageParameters)
    {
        var players = await _dataContext.Players.Where(x=>x.isBanned == false)
            .OrderByDescending(player => player.Points).Select(x=> _playerMapper.ToPlayerResponse(x))
            .Skip(pageParameters.PageNumber * pageParameters.PageSize)
            .Take(pageParameters.PageSize)
            .ToListAsync();

        return  players ;
    }

    public async Task<long> GetPlayerPoint(long playerId)
    {
        var res = await _dataContext.Players.FindAsync(playerId);
        if (res == null) return 0;
        return res.Points;
    }

    public async Task UpdatePointsForPlayerNoSave(long playerId, long points,long tourId)
    {
        var res = await _dataContext.Players.FindAsync(playerId);
        if (res == null) return;
       
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
        
    }

    public async Task<int> NumberOfLeaderBoard()
    {
        return await _dataContext.Players.Where(x=>x.isBanned == false).CountAsync();
    }
}