using Shared.DataAccess.DTO;
using Shared.DataAccess.DTO.Requests;
using Shared.DataAccess.DTO.Responses;
using Shared.DataAccess.Pagination;
using Shared.Results;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Shared.DataAccess.RepositoryInterfaces
{
	public interface IGameService
    {
        Task<HandlerResult<SuccessData<PageResponse<GameResponse>>,IErrorResult>> GetGames(PageParameters pageParameters);

        Task<HandlerResult<SuccessData<GameResponse>,IErrorResult>> GetGame(long id);

        Task<HandlerResult<Success,IErrorResult>>  ModifyGameType(long id, GameRequest gameRequest);

        Task<HandlerResult<Success,IErrorResult>> DeleteGame(long id);

        Task<HandlerResult<Success,IErrorResult>> CreateGameType(long? userId,GameRequest gameRequest);
        Task<HandlerResult<SuccessData<PageResponse<GameResponse>>, IErrorResult>> GetGamesByPlayer(string? name, PageParameters pageParameters);
        Task<HandlerResult<SuccessData<PageResponse<GameResponse>>, IErrorResult>> GetListOfTypesOfAvailableGames(PageParameters pageParameters);
        Task<HandlerResult<SuccessData<PageResponse<GameResponse>>, IErrorResult>> Search(string? name, PageParameters pageParameters);
    }
}
