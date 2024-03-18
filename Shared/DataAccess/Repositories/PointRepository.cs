﻿using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Shared.DataAccess.Context;
using Shared.DataAccess.DTO;
using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.Mappers;
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

    public PointRepository(DataContext dataContext, IPointHistoryMapper pointHistoryMapper)
    {
        _dataContext = dataContext;
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

    public async Task<HandlerResult<Success, IErrorResult>> SetPointsForPlayer(long playerId, long points)
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
        if (player.Points > points)
        {
            return await SubtractPoints(player, player.Points - points);
        }
        if (player.Points < points)
        {
            return await AddPoints(player, points - player.Points);
        }

        return new Success();
    }

    private async Task<HandlerResult<Success, IErrorResult>> AddPoints(Player player, long points)
    {
        player.Points += points;
        var pointHistory = new PointHistory()
        {
            PlayerId = player.Id,
            Gain = points,
            Loss = 0,
            LogDate = DateTime.Now
        };
        await _dataContext.PointHistories.AddAsync(pointHistory);
        await _dataContext.SaveChangesAsync();
        return new Success();
    }

    private async Task<HandlerResult<Success, IErrorResult>> SubtractPoints(Player player, long points)
    {
        player.Points -= points;
        var pointHistory = new PointHistory
        {
            PlayerId = player.Id,
            Gain = 0,
            Loss = points,
            LogDate = DateTime.Now
        };
        await _dataContext.PointHistories.AddAsync(pointHistory);
        await _dataContext.SaveChangesAsync();
        return new Success();
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

    public async Task<HandlerResult<SuccessData<List<Player>>, IErrorResult>> GetLeaderboards()
    {
        var players = await _dataContext.Players
            .OrderByDescending(player => player.Points)
            .Take(10)
            .ToListAsync();

        return new SuccessData<List<Player>>
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

    public async Task<HandlerResult<Success, IErrorResult>> UpdatePointsForPlayerNoSave(long playerId, long points)
    {
        var res = await _dataContext.Players.FindAsync(playerId);
        if (res == null) return new EntityNotFoundErrorResult();
        res.Points += points;
        var pointHistory = new PointHistory()
        {
            PlayerId = playerId,
            Gain = points > 0 ? points : 0,
            Loss = points <= 0 ? points : 0,
            LogDate = DateTime.Now
        };
        _dataContext.Players.Update(res);
        await _dataContext.PointHistories.AddAsync(pointHistory);
        
        return new Success();
    }
}