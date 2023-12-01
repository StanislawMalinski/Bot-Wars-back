using Microsoft.EntityFrameworkCore;
using Shared.DataAccess.Context;
using Shared.DataAccess.DAO;
using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.Mappers;
using Shared.DataAccess.RepositoryInterfaces;
using Shared.DataAccess.Services.Results;

namespace Shared.DataAccess.Repositories;

public class GameRepository : IGameRepository
{
    private readonly DataContext _dataContext;
    private readonly IGameTypeMapper _mapper;

    public GameRepository(DataContext dataContext, IGameTypeMapper gameTypeMapper)
    {
        _mapper = gameTypeMapper;
        _dataContext = dataContext;
    }

    public async Task<ServiceResponse<GameDto>> CreateGameType(GameDto game)
    {
        try
        {
            var resGame = await _dataContext.Games.AddAsync(_mapper.ToGameType(game));
			await _dataContext.SaveChangesAsync();
			return new ServiceResponse<GameDto>()
            {
                Data = _mapper.ToDto(resGame.Entity),
                Message = "Game added",
                Success = true
            };
            
        }
        catch (Exception e)
        {
            return new ServiceResponse<GameDto>()
            {
                Data = null,
                Message = "Problem with database",
                Success = false
            };
        }
    }

    public async Task<ServiceResponse<List<GameDto>>> GetGameTypes()
    {
        try
        {
            var resGame = await _dataContext.Games.ToListAsync();
            return new ServiceResponse<List<GameDto>>()
            {
				Data = resGame.Select(x => _mapper.ToDto(x)).ToList(),
				Message = "games",
                Success = true
            };
            
        }
        catch (Exception e)
        {
            return new ServiceResponse<List<GameDto>>()
            {
                Data = null,
                Message = "Problem with database",
                Success = false
            };
        }
    }

    public async Task<ServiceResponse<GameDto>> DeleteGame(long id)
    {
        try
        {
            
            var resGame = await _dataContext.Games.FindAsync(id);
            _dataContext.Remove(resGame);
            await _dataContext.SaveChangesAsync();
            return new ServiceResponse<GameDto>()
            {
                Data = _mapper.ToDto(resGame),
                Message = "Game deleted",
                Success = true
            };
            
        }
        catch (Exception e)
        {
            return new ServiceResponse<GameDto>()
            {
                Data = null,
                Message = "Problem with database",
                Success = false
            };
        }
    }

    public async Task<ServiceResponse<GameDto>> GetGameType(long id)
    {
        try
        {
            var resGame = await _dataContext.Games.FindAsync(id);
            return new ServiceResponse<GameDto>()
            {
                Data = _mapper.ToDto(resGame),
                Message = "Game found",
                Success = true
            };
            
        }
        catch (Exception e)
        {
            return new ServiceResponse<GameDto>()
            {
                Data = null,
                Message = "Problem with database",
                Success = false
            };
        }
    }

    public async Task<ServiceResponse<GameDto>> ModifyGameType(long id, GameDto gameDto)
    {
        try
        {
            var game = _mapper.ToGameType(gameDto);
            game.Id = id;
            _dataContext.Update(game);
            await _dataContext.SaveChangesAsync();
            return new ServiceResponse<GameDto>()
            {
                Data = gameDto,
                Message = "Game changed",
                Success = true
            };
            
        }
        catch (Exception e)
        {
            return new ServiceResponse<GameDto>()
            {
                Data = null,
                Message = "Problem with database",
                Success = false
            };
        }
    }

    
}