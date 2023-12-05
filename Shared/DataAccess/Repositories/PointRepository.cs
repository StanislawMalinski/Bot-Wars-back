using Microsoft.EntityFrameworkCore;
using Shared.DataAccess.Context;
using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.RepositoryInterfaces;
using Shared.Results;
using Shared.Results.ErrorResults;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Shared.DataAccess.Repositories;

public class PointRepository : IPointsRepository
{
    private DataContext _dataContext;
    public PointRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }
    
    public async Task<HandlerResult<Success, IErrorResult>> setPointsForPlayer(long PlayerId, long Points)
    {
        var player = await _dataContext.Players.FindAsync(PlayerId);
        if (player.Points > Points)
        {
            return await subtracPoints(PlayerId, player.Points - Points);
        }
        else
        {
            return await addPoints(PlayerId, Points - player.Points);
        }
    }

    public async Task<HandlerResult<Success, IErrorResult>> addPoints(long PlayerId, long Points)
    {
        var player = await _dataContext.Players.FindAsync(PlayerId);
        if (player == null)
        {
            return new EntityNotFoundErrorResult()
            {
                Title = "return null",
                Message = "Nie ma gracza z tym id"
            };
        }
        player.Points += Points;
        _dataContext.Players.Update((player));
        PointHistory pointHistory = new PointHistory()
        {
            PlayerId = player.Id,
            Gain = Points,
            Loss = 0,
            LogDate = DateTime.Now
        };
        await _dataContext.PointHistories.AddAsync(pointHistory);
        await _dataContext.SaveChangesAsync();
        return new Success();
    }

    public async Task<HandlerResult<Success, IErrorResult>> subtracPoints(long PlayerId, long Points)
    {
        var player = await _dataContext.Players.FindAsync(PlayerId);
        if (player == null)
        {
            return new EntityNotFoundErrorResult()
            {
                Title = "return null",
                Message = "Nie ma gracza z tym id"
            };
        }
        player.Points -= Points;
        _dataContext.Players.Update((player));
        PointHistory pointHistory = new PointHistory()
        {
            PlayerId = player.Id,
            Gain = 0,
            Loss = Points,
            LogDate = DateTime.Now
        };
        await _dataContext.PointHistories.AddAsync(pointHistory);
        await _dataContext.SaveChangesAsync();
        return new Success();
    }

    public async Task<HandlerResult<SuccessData<long>, IErrorResult>> getCurrentPointForPlayer(long PlayerId)
    {
        var player = await _dataContext.Players.FindAsync(PlayerId);
        if (player == null)
        {
            return new EntityNotFoundErrorResult()
            {
                Title = "return null",
                Message = "Nie ma gracza z tym id"
            };
        }

        return new SuccessData<long>()
        {
            Data = player.Points
        };
    }

    public async Task<HandlerResult<SuccessData<List<Player>>, IErrorResult>> getLeadboards()
    {
        var players =  from player in _dataContext.Players
                orderby player.Points
                select player;
        
        return new SuccessData<List<Player>>()
        {
            Data = players.ToList()
        };
    }
}