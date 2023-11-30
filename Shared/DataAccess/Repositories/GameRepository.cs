using Microsoft.EntityFrameworkCore;
using Shared.DataAccess.Context;
using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.RepositoryInterfaces;
using Shared.DataAccess.Services.Results;

namespace Shared.DataAccess.Repositories;

public class GameRepository : IGameRepository
{
    private readonly DataContext _dataContext;

    public GameRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<ServiceResponse<Game>> CreateGameType(Game game)
    {
        try
        {
            var resGame = await _dataContext.Games.AddAsync(game);
            return new ServiceResponse<Game>()
            {
                Data = resGame.Entity,
                Message = "Game added",
                Success = true
            };
            
        }
        catch (Exception e)
        {
            return new ServiceResponse<Game>()
            {
                Data = null,
                Message = "Problem with database",
                Success = false
            };
        }
    }

    public async Task<ServiceResponse<List<Game>>> GetGameTypes()
    {
        try
        {
            var resGame = await _dataContext.Games.ToListAsync();
            return new ServiceResponse<List<Game>>()
            {
                Data = resGame,
                Message = "games",
                Success = true
            };
            
        }
        catch (Exception e)
        {
            return new ServiceResponse<List<Game>>()
            {
                Data = null,
                Message = "Problem with database",
                Success = false
            };
        }
    }

    public async Task<ServiceResponse<Game>> DeleteGame(long id)
    {
        try
        {
            
            var resGame = await _dataContext.Games.FindAsync(id);
            _dataContext.Remove(resGame);
            await _dataContext.SaveChangesAsync();
            return new ServiceResponse<Game>()
            {
                Data = resGame,
                Message = "Game deleted",
                Success = true
            };
            
        }
        catch (Exception e)
        {
            return new ServiceResponse<Game>()
            {
                Data = null,
                Message = "Problem with database",
                Success = false
            };
        }
    }

    public async Task<ServiceResponse<Game>> GetGameType(long id)
    {
        try
        {
            var resGame = await _dataContext.Games.FindAsync(id);
            return new ServiceResponse<Game>()
            {
                Data = resGame,
                Message = "Game found",
                Success = true
            };
            
        }
        catch (Exception e)
        {
            return new ServiceResponse<Game>()
            {
                Data = null,
                Message = "Problem with database",
                Success = false
            };
        }
    }

    public async Task<ServiceResponse<Game>> ModifyGameType(long id, Game game)
    {
        try
        {
          
            game.Id = id;
            _dataContext.Update(game);
            await _dataContext.SaveChangesAsync();
            return new ServiceResponse<Game>()
            {
                Data = game,
                Message = "Game changed",
                Success = true
            };
            
        }
        catch (Exception e)
        {
            return new ServiceResponse<Game>()
            {
                Data = null,
                Message = "Problem with database",
                Success = false
            };
        }
    }

    
}