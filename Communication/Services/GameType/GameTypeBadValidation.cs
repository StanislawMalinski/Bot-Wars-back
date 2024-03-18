using Communication.Services.Validation;
using Shared.DataAccess.DTO;
using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.DTO.Requests;
using Shared.DataAccess.DTO.Responses;
using Shared.DataAccess.RepositoryInterfaces;
using Shared.Results;
using Shared.Results.ErrorResults;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Communication.Services.GameType
{
	public class GameTypeBadValidation : IGameService
	{
		public async Task<HandlerResult<Success,IErrorResult>> CreateGameType(GameRequest game)
		{
			
			return new AccessDeniedError();
		}

		public async Task<HandlerResult<SuccessData<List<GameResponse>>, IErrorResult>> GetListOfTypesOfAvailableGames()
		{
			return new AccessDeniedError();
		}

		public async Task<HandlerResult<Success,IErrorResult>> DeleteGame(long id)
		{
			
			return new AccessDeniedError();
		}

		public async Task<HandlerResult<SuccessData<List<GameResponse>>,IErrorResult>> GetGames()
		{
			
			return new AccessDeniedError();
		}

		public async Task<HandlerResult<SuccessData<GameResponse>, IErrorResult>> GetGame(long id)
		{
			return new AccessDeniedError();
		}

		public async Task<HandlerResult<Success,IErrorResult>> ModifyGameType(long id, GameRequest gameDto)
		{
			
			return new AccessDeniedError();
		}

	}
}
