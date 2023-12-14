using Shared.DataAccess.DAO;
using Shared.DataAccess.DataBaseEntities;
using Shared.Results;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Shared.DataAccess.RepositoryInterfaces
{
	public interface IPlayerService
    {
        public Task<HandlerResult<Success,IErrorResult>> CreatePlayerAsync(PlayerDto player);
        public Task<HandlerResult<Success,IErrorResult>> DeletePlayerAsync(long id);
        public Task<HandlerResult<SuccessData<Player>,IErrorResult>> GetPlayerAsync(long id);
        public Task<HandlerResult<SuccessData<List<Player>>,IErrorResult>> GetPlayersAsync();
        public Task<HandlerResult<Success,IErrorResult>> UpdatePlayerAsync(PlayerDto player);

    }
}
