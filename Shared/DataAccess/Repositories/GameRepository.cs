using Microsoft.EntityFrameworkCore;
using Shared.DataAccess.Context;
using Shared.DataAccess.DAO;
using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.Mappers;
using Shared.DataAccess.RepositoryInterfaces;
using Shared.Results;
using Shared.Results.ErrorResults;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

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


    public async Task<HandlerResult<Success, IErrorResult>> CreateGameType(GameDto game)
    {
        
        var resGame = await _dataContext.Games.AddAsync(_mapper.ToGameType(game));
        await _dataContext.SaveChangesAsync();
        return new Success();

    }

    public async Task<HandlerResult<SuccessData<List<GameDto>>, IErrorResult>> GetGameTypes()
    {
        var resGame = _dataContext.Games.Select(x => _mapper.ToDto(x)).ToList();
        
        return new SuccessData<List<GameDto>>()
        {
            Data = resGame
        };

    }

    public async Task<HandlerResult<Success, IErrorResult>> DeleteGame(long id)
    {
        
        var resGame = await _dataContext.Games.FindAsync(id);
        if (resGame == null)
        {
            return new EntityNotFoundErrorResult()
            {
                Title = "Return null",
                Message = "Nie ma elemętu w bazie danych"
            };
        }
        _dataContext.Remove(resGame);
        await _dataContext.SaveChangesAsync();
        return new Success();


    }

    public async Task<HandlerResult<SuccessData<GameDto>, IErrorResult>> GetGameType(long id)
    {
        
        var resGame = await _dataContext.Games.FindAsync(id);
        if (resGame == null)
        {
            return new EntityNotFoundErrorResult()
            {
                Title = "return null",
                Message = "Nie znaleziono elementu o tym id"
            };
        }

        return new SuccessData<GameDto>()
        {
            Data = _mapper.ToDto(resGame)
        };

    }

    public async Task<HandlerResult<Success, IErrorResult>> ModifyGameType(long id, GameDto gameDto)
    {
        var resGame = await _dataContext.Games.FindAsync(id);
        if (resGame == null)
        {
            return new EntityNotFoundErrorResult()
            {
                Title = "Return null",
                Message = "Nie ma elemętu w bazie danych"
            };
        }
        var game = _mapper.ToGameType(gameDto);
        game.Id = id;
        _dataContext.Update(game);
        await _dataContext.SaveChangesAsync();
        return new Success();
    }
}