using Shared.DataAccess.DAO;
using Shared.DataAccess.DataBaseEntities;
using Shared.Results;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Shared.DataAccess.RepositoryInterfaces
{
	public interface IGameService
    {
        public Task<HandlerResult<SuccessData<List<GameDto>>,IErrorResult>> GetGameTypes();

        //public Task<HandlerResult<SuccessData<GameDto>,IErrorResult>> GetGameAsync(long id);

        public Task<HandlerResult<Success,IErrorResult>>  ModifyGameType(long id, GameDto gameDto);

        public Task<HandlerResult<Success,IErrorResult>> DeleteGame(long id);

        public Task<HandlerResult<Success,IErrorResult>> CreateGameType(GameDto gameDto);
        
        
        
      
    }


}
