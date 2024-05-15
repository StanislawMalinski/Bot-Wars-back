using Microsoft.EntityFrameworkCore.ChangeTracking;
using Shared.DataAccess.DAO;
using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.DTO;
using Shared.DataAccess.DTO.Requests;
using Shared.DataAccess.DTO.Responses;
using Shared.DataAccess.Pagination;
using Shared.Results;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Shared.DataAccess.RepositoryInterfaces;

public interface IGameRepository
{
    public Task<List<GameResponse>> GetGamesByPlayer(long playerId, PageParameters pageParameters);
    public Task<bool> CreateGameType(long? userId,GameRequest gameRequest);
    public Task<List<GameResponse>> GetGames(PageParameters pageParameters);
    public Task<bool> DeleteGame(long id);
 
    public Task<bool> ModifyGameType(long id, GameRequest gameRequest);
    public Task<List<GameResponse>> GetAvailableGames(PageParameters pageParameters);
    public Task<List<GameResponse>> Search(string? name, PageParameters pageParameters);
    public Task<bool> GameNotAvailableForPlay(long gameId);
    public Task<long?> GetCreatorId(long gameId);
    
    Task<bool> DeleteGameAsync(long id);
    Task<Game?> GetGame(long gameId);
    Task<Game?> GetGameIncluded(long gameId);
    Task SaveChangesAsync();
    Task<EntityEntry<Game>> AddPGame(Game game);

}