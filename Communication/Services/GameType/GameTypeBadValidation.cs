using Communication.Services.Validation;
using Shared.DataAccess.DAO;
using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.RepositoryInterfaces;
using Shared.Results;
using Shared.Results.ErrorResults;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Communication.Services.GameType
{
	public class GameTypeBadValidation : IGameService
	{
		public async Task<HandlerResult<Success,IErrorResult>> CreateGameType(GameDto game)
		{
			
			return new AccessDeniedError();
		}

		public async Task<HandlerResult<Success,IErrorResult>> DeleteGame(long id)
		{
			
			return new AccessDeniedError();
		}

		public async Task<HandlerResult<SuccessData<List<GameDto>>,IErrorResult>> GetGameTypes()
		{
			
			return new AccessDeniedError();
		}

		public async Task<HandlerResult<Success,IErrorResult>> ModifyGameType(long id, GameDto gameDto)
		{
			
			return new AccessDeniedError();
		}

	}
}
