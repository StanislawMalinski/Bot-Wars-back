using BotWars.Services;
using Microsoft.EntityFrameworkCore;
using Shared.DataAccess.Context;
using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.RepositoryInterfaces;

namespace Shared.DataAccess.Repositories;

public class PointRepository : IPointsRepository
{
    private DataContext _dataContext;
    public PointRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<ServiceResponse<bool>> setPointsForPlayer(long PlayerId, long Points)
    {
       
        try
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
        catch (Exception e)
        {
            return new ServiceResponse<bool>()
            {
                Data = false,
                Message = "Problem with database",
                Success = false
            };
        }

        
    }

    public async Task<ServiceResponse<bool>> addPoints(long PlayerId, long Points)
    {
        
        try
        {
            var player = await _dataContext.Players.FindAsync(PlayerId);
            player.Points += Points;
            _dataContext.Players.Update((player));
            PointHistory pointHistory = new PointHistory()
            {
                PlayerId = player.Id,
                Gain = Points,
                Loss = 0,
                LogDate = DateTime.Now
            };
            _dataContext.PointHistories.AddAsync(pointHistory);
            _dataContext.SaveChangesAsync();
            return new ServiceResponse<bool>()
            {
                Data = true,
                Message = "Points added",
                Success = true
            };
        }
        catch (Exception e)
        {
            return new ServiceResponse<bool>()
            {
                Data = false,
                Message = "Problem with database",
                Success = false
            };
        }
    }

    public async Task<ServiceResponse<bool>> subtracPoints(long PlayerId, long Points)
    {
        
        try
        {
            var player = await _dataContext.Players.FindAsync(PlayerId);
            player.Points -= Points;
            _dataContext.Players.Update((player));
            PointHistory pointHistory = new PointHistory()
            {
                PlayerId = player.Id,
                Gain = 0,
                Loss = Points,
                LogDate = DateTime.Now
            };
            _dataContext.PointHistories.AddAsync(pointHistory);
            _dataContext.SaveChangesAsync();
            return new ServiceResponse<bool>()
            {
                Data = true,
                Message = "Points subtracted",
                Success = true
            };
        }
        catch (Exception e)
        {
            return new ServiceResponse<bool>()
            {
                Data = false,
                Message = "Problem with database",
                Success = false
            };
        }
    }

    public async Task<ServiceResponse<long>> getCurrentPointForPlayer(long PlayerId)
    {
       
        try
        {
            var player = await _dataContext.Players.FindAsync(PlayerId);
            return new ServiceResponse<long>()
            {
                Data = player.Points,
                Message = "Points",
                Success = true
            };
            
        }
        catch (Exception e)
        {
            return new ServiceResponse<long>()
            {
                Data = 0,
                Message = "Problem with database",
                Success = false
            };
        }
    }

    public async Task<ServiceResponse<List<Player>>> getLeadboards()
    {
        
        try
        {
            var players = await _dataContext.Players.ToListAsync();
            players.Sort(Compare);
            return new ServiceResponse<List<Player>>()
            {
                Data = players,
                Message = "Leaderbord",
                Success = true
            };
        }
        catch (Exception e)
        {
            return new ServiceResponse<List<Player>>()
            {
                Data = null,
                Message = "Problem with database",
                Success = false
            };
        }
    }

    private static int Compare(Player p1 ,Player p2)
    {
        if (p1.Points > p2.Points)
        {
            return -1;
        }

        return 1;
    }
}