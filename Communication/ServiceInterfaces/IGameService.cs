using Shared.DataAccess.DTO;
using Shared.DataAccess.DTO.Requests;
using Shared.DataAccess.DTO.Responses;
using Shared.Results;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Shared.DataAccess.RepositoryInterfaces
{
	public interface IGameService
    {
        Task<HandlerResult<SuccessData<List<GameResponse>>,IErrorResult>> GetGames(int page, int pagesize);

        Task<HandlerResult<SuccessData<GameResponse>,IErrorResult>> GetGame(long id);

        Task<HandlerResult<Success,IErrorResult>>  ModifyGameType(long id, GameRequest gameRequest);

        Task<HandlerResult<Success,IErrorResult>> DeleteGame(long id);

        Task<HandlerResult<Success,IErrorResult>> CreateGameType(long? userId,GameRequest gameRequest);
        Task<HandlerResult<SuccessData<List<GameResponse>>, IErrorResult>> GetGamesByPlayer(string? name);
        Task<HandlerResult<SuccessData<List<GameResponse>>, IErrorResult>> GetListOfTypesOfAvailableGames(int page, int pagesize);
        Task<HandlerResult<SuccessData<List<GameResponse>>, IErrorResult>> Search(string? name, int page, int pagesize);
    }
}
