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
    public Task<HandlerResult<SuccessData<List<GameResponse>>, IErrorResult>> GetGamesByPlayer(string? name, PageParameters pageParameters);
    public Task<HandlerResult<Success, IErrorResult>> CreateGameType(long? userId,GameRequest gameRequest);
    public Task<HandlerResult<SuccessData<List<GameResponse>>, IErrorResult>> GetGames(PageParameters pageParameters);
    public Task<HandlerResult<Success, IErrorResult>> DeleteGame(long id);
    public Task<HandlerResult<SuccessData<GameResponse>, IErrorResult>> GetGame(long id);
    public Task<HandlerResult<Success, IErrorResult>> ModifyGameType(long id, GameRequest gameRequest);
    public Task<HandlerResult<SuccessData<List<GameResponse>>, IErrorResult>> GetAvailableGames(PageParameters pageParameters);
    public Task<HandlerResult<SuccessData<List<GameResponse>>, IErrorResult>> Search(string? name, PageParameters pageParameters);
    public Task<HandlerResult<Success, IErrorResult>> GameNotAvailableForPlay(long gameId);
    public Task<long?> GetCreatorId(long gameId);
    
    Task<bool> DeleteGameAsync(long id);
    Task<Game?> GetGame1(long gameId);
    Task SaveChangesAsync();
    Task<EntityEntry<Game>> AddPGame(Game game);

}