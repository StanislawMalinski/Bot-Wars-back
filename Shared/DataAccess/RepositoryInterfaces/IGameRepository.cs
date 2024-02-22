using Shared.DataAccess.DTO;
using Shared.Results;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Shared.DataAccess.RepositoryInterfaces;

public interface IGameRepository
{
    public Task<HandlerResult<Success,IErrorResult>> CreateGameType(GameDto game);
    public Task<HandlerResult<SuccessData< List<GameDto>>,IErrorResult>> GetGameTypes();
    public Task<HandlerResult<Success,IErrorResult>> DeleteGame(long id);
    public Task<HandlerResult<SuccessData<GameDto>,IErrorResult>> GetGameType(long id);
    public Task<HandlerResult<Success,IErrorResult>> ModifyGameType(long id, GameDto gameDto);
}