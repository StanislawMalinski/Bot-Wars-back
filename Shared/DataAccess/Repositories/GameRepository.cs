using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Shared.DataAccess.Context;
using Shared.DataAccess.DTO.Requests;
using Shared.DataAccess.DTO.Responses;
using Shared.DataAccess.Mappers;
using Shared.DataAccess.RepositoryInterfaces;
using Shared.Results;
using Shared.Results.ErrorResults;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;
using Shared.DataAccess.DataBaseEntities;
using System.Net.Http;

namespace Shared.DataAccess.Repositories;

public class GameRepository : IGameRepository
{
    private readonly DataContext _dataContext;
    private readonly IGameTypeMapper _mapper;
    private readonly HttpClient _httpClient;
    // move to config
    private readonly string _gathererEndpoint = "http://host.docker.internal:7002/api/Gatherer";

    public GameRepository(DataContext dataContext, IGameTypeMapper gameTypeMapper, HttpClient httpClient)
    {
        _mapper = gameTypeMapper;
        _dataContext = dataContext;
        _httpClient = httpClient;
    }


    public async Task<HandlerResult<Success, IErrorResult>> CreateGameType(long userId,GameRequest gameRequest)
    {
    
        var content = new MultipartFormDataContent();
        var streamContent = new StreamContent(gameRequest.GameFile.OpenReadStream());
        content.Add(streamContent, "file", gameRequest.GameFile.FileName);
        var res = await _httpClient.PutAsync(_gathererEndpoint, content);
        long gameFileId;
        if (res.IsSuccessStatusCode)
        {
            string cont = await res.Content.ReadAsStringAsync();
            gameFileId = Convert.ToInt32(cont);
        }
        else
        {
            return new IncorrectOperation
            {
                Title = "IncorrectOperation 404",
                Message = "Faild to upload file to FileGatherer"
            };
        }
        Game game = _mapper.MapRequestToGame(gameRequest);
        game.FileId = gameFileId;
        game.CreatorId = userId;
        await _dataContext
            .Games
            .AddAsync(game);
        await _dataContext.SaveChangesAsync();
        return new Success();
        

    }

    public async Task<HandlerResult<SuccessData<List<GameResponse>>, IErrorResult>> GetGames()
    {
        var resGame = await _dataContext
            .Games
            .Include(game => game.Bot)
            .Include(game => game.Matches)
            .Include(game => game.Tournaments)
            .Select(x => _mapper.MapGameToResponse(x))
            .ToListAsync();
        

        return new SuccessData<List<GameResponse>>()
        {
            Data = resGame
        };

    }

    public async Task<HandlerResult<Success, IErrorResult>> DeleteGame(long id)
    {
        
        var gameToRemove = await _dataContext
            .Games
            .Include(g => g.Tournaments)
            .FirstOrDefaultAsync(g => g.Id == id);
        
        if (gameToRemove == null)
        {
            return new EntityNotFoundErrorResult()
            {
                Title = "EntityNotFoundErrorResult 404",
                Message = "No such element could have been found"
            };
        }

        if (gameToRemove.Tournaments != null) _dataContext
            .Tournaments
            .RemoveRange(gameToRemove.Tournaments);
        _dataContext.Games.Remove(gameToRemove);
        await _dataContext.SaveChangesAsync();
        
        return new Success();
    }

    public async Task<HandlerResult<SuccessData<GameResponse>, IErrorResult>> GetGame(long id)
    {

        var resGame = await _dataContext
            .Games
            .Where(game => game.Id==id)
            .Include(game => game.Bot)
            .Include(game => game.Matches)
            .Include(game => game.Tournaments)
            .FirstOrDefaultAsync();
        
        if (resGame == null)
        {
            return new EntityNotFoundErrorResult()
            {
                Title = "EntityNotFoundErrorResult 404",
                Message = "No such element could have been found"
            };
        }

        return new SuccessData<GameResponse>()
        {
            Data = _mapper.MapGameToResponse(resGame)
        };

    }

    public async Task<HandlerResult<Success, IErrorResult>> ModifyGameType(long id, GameRequest gameRequest)
    {
        var resGame = await _dataContext.Games.FindAsync(id);
        if (resGame == null)
        {
            return new EntityNotFoundErrorResult()
            {
                Title = "EntityNotFoundErrorResult 404",
                Message = "No such element could have been found"
            };
        }
        resGame.InterfaceDefinition = gameRequest.InterfaceDefinition;
        resGame.GameInstructions = gameRequest.GameInstructions;
        resGame.GameFile = gameRequest.GameFile?.FileName;
        resGame.NumbersOfPlayer = gameRequest.NumberOfPlayer;
        resGame.LastModification = DateTime.Now;
        resGame.IsAvailableForPlay = gameRequest.IsAvailableForPlay;
        await _dataContext.SaveChangesAsync();
        return new Success();
    }

    public async Task<HandlerResult<SuccessData<List<GameResponse>>, IErrorResult>> GetAvailableGames()
    {
        var resGame = await _dataContext
            .Games
            .Include(game => game.Bot)
            .Include(game => game.Matches)
            .Include(game => game.Tournaments)
            .Where(game => game.IsAvailableForPlay)
            .Select(game => _mapper.MapGameToResponse(game))
            .ToListAsync();
        

        return new SuccessData<List<GameResponse>>()
        {
            Data = resGame
        };
    }
}