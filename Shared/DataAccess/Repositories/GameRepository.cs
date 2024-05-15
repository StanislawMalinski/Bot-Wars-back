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
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Shared.DataAccess.AuthorizationRequirements;
using Shared.DataAccess.Pagination;

namespace Shared.DataAccess.Repositories;

public class GameRepository : IGameRepository
{
    private readonly DataContext _dataContext;
    private readonly IGameTypeMapper _mapper;

    private readonly HttpClient _httpClient;

    // move to config
    private readonly string _gathererEndpoint = "http://host.docker.internal:7002/api/Gatherer";


    public GameRepository(DataContext dataContext,
        IGameTypeMapper gameTypeMapper,
        HttpClient httpClient
    )
    {
        _mapper = gameTypeMapper;
        _dataContext = dataContext;
        _httpClient = httpClient;
    }

    
    public async Task<bool> CreateGameType(long? userId, GameRequest gameRequest)
    {
        if (userId is null) return false;

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
            return false;
        }

        Game game = _mapper.MapRequestToGame(gameRequest);
        game.FileId = gameFileId;
        game.CreatorId = (long)userId;
        await _dataContext
            .Games
            .AddAsync(game);
        await _dataContext.SaveChangesAsync();
        return true;
    }

    public async Task<List<GameResponse>> Search(string? name,
        PageParameters pageParameters)
    {
        
        return await _dataContext
            .Games
            .Where(x => x.GameFile != null && x.GameFile.Contains(name))
            .Skip(pageParameters.PageNumber * pageParameters.PageSize)
            .Take(pageParameters.PageSize)
            .Select(x=>_mapper.MapGameToResponse(x))
            .ToListAsync();
        
    }

    public async Task<List<GameResponse>> GetGamesByPlayer(long playerId, PageParameters pageParameters)
    {
        return await _dataContext
            .Games
            .Where(x => x.GameFile != null && x.CreatorId == playerId)
            .Skip(pageParameters.PageNumber * pageParameters.PageSize)
            .Take(pageParameters.PageSize)
            .Select(x=>_mapper.MapGameToResponse(x))
            .ToListAsync();
    }

    public async Task<List<GameResponse>> GetGames(PageParameters pageParameters)
    {
        return await _dataContext
            .Games
            .Include(game => game.Bot)
            .Include(game => game.Matches)
            .Include(game => game.Tournaments)
            .Skip(pageParameters.PageNumber * pageParameters.PageSize)
            .Take(pageParameters.PageSize)
            .Select(x => _mapper.MapGameToResponse(x))
            .ToListAsync();
    }

    public async Task<bool> DeleteGame(long id)
    {
        var gameToRemove = await _dataContext
            .Games
            .Include(g => g.Tournaments)
            .FirstOrDefaultAsync(g => g.Id == id);
        
        if (gameToRemove == null) return false;

        if (gameToRemove.Tournaments != null)
            _dataContext
                .Tournaments
                .RemoveRange(gameToRemove.Tournaments);
        _dataContext.Games.Remove(gameToRemove);
        return true;
    }
    

    public async Task<bool> ModifyGameType(long id, GameRequest gameRequest)
    {
        var resGame = await _dataContext.Games.FindAsync(id);
        if (resGame == null) return false;
        resGame.InterfaceDefinition = gameRequest.InterfaceDefinition;
        resGame.GameInstructions = gameRequest.GameInstructions;
        resGame.GameFile = gameRequest.GameFile?.FileName;
        resGame.NumbersOfPlayer = gameRequest.NumberOfPlayer;
        resGame.LastModification = DateTime.Now;
        resGame.IsAvailableForPlay = gameRequest.IsAvailableForPlay;
        return true;
    }

    public async Task<List<GameResponse>> GetAvailableGames(
        PageParameters pageParameters)
    {
        
        return await _dataContext
            .Games
            .Include(game => game.Bot)
            .Include(game => game.Matches)
            .Include(game => game.Tournaments)
            .Where(game => game.IsAvailableForPlay)
            .Skip(pageParameters.PageNumber * pageParameters.PageSize)
            .Take(pageParameters.PageSize)
            .Select(game => _mapper.MapGameToResponse(game))
            .ToListAsync();
        
    }

    public async Task<bool> GameNotAvailableForPlay(long gameId)
    {
        var res = await _dataContext.Games.FindAsync(gameId);
        if (res == null) return false;
        res.IsAvailableForPlay = false;
        return true;
    }

    public async Task<long?> GetCreatorId(long gameId)
    {
        var resGame = await _dataContext
            .Games
            .FirstOrDefaultAsync(game => game.Id == gameId);
        return resGame?.CreatorId;
    }

    public async Task<bool> DeleteGameAsync(long id)
    {
        //_dataContext.Games.re
        return false;
    }

    public async Task<Game?> GetGame(long gameId)
    {
        return await _dataContext.Games.FindAsync(gameId);
    }
    
    public async Task<Game?> GetGameIncluded(long gameId)
    {
        return await _dataContext.Games.Where(game => game.Id == gameId)
            .Include(game => game.Bot)
            .Include(game => game.Matches)
            .Include(game => game.Tournaments)
            .FirstOrDefaultAsync();;
    }
    
   

    public async Task SaveChangesAsync()
    {
        await _dataContext.SaveChangesAsync();
    }

    public async Task<EntityEntry<Game>> AddPGame(Game game)
    {
        return await _dataContext.Games.AddAsync(game);
    }
}