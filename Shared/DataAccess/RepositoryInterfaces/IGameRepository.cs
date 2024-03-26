using Shared.DataAccess.DAO;
using Shared.DataAccess.DTO;
using Shared.DataAccess.DTO.Requests;
using Shared.DataAccess.DTO.Responses;
using Shared.Results;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Shared.DataAccess.RepositoryInterfaces;

public interface IGameRepository
{
    public Task<HandlerResult<Success, IErrorResult>> CreateGameType(long userId,GameRequest gameRequest);
    public Task<HandlerResult<SuccessData<List<GameResponse>>, IErrorResult>> GetGames();
    public Task<HandlerResult<Success, IErrorResult>> DeleteGame(long id);
    public Task<HandlerResult<SuccessData<GameResponse>, IErrorResult>> GetGame(long id);
    public Task<HandlerResult<Success, IErrorResult>> ModifyGameType(long id, GameRequest gameRequest);
    public Task<HandlerResult<SuccessData<List<GameResponse>>, IErrorResult>> GetAvailableGames();
    public Task<HandlerResult<SuccessData<List<GameResponse>>, IErrorResult>> Search(string? name);
}