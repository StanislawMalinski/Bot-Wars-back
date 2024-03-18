using Shared.DataAccess.DTO;
using Shared.DataAccess.DTO.Requests;
using Shared.DataAccess.DTO.Responses;
using Shared.DataAccess.RepositoryInterfaces;
using Shared.Results;
using Shared.Results.ErrorResults;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Communication.Services.GameType
{
	public class GameTypeUnidentifiedPlayerService : IGameService
	{
		protected readonly GameTypeServiceProvider _gameTypeServiceProvider;

		public GameTypeUnidentifiedPlayerService(GameTypeServiceProvider gameTypeServiceProvider)
		{
			_gameTypeServiceProvider = gameTypeServiceProvider;
		}
		
		public virtual async Task<HandlerResult<SuccessData<List<GameResponse>>, IErrorResult>> GetGames()
		{
			return await _gameTypeServiceProvider.GetGames();
		}

		public async Task<HandlerResult<SuccessData<GameResponse>, IErrorResult>> GetGame(long id)
		{
			return await _gameTypeServiceProvider.GetGame(id);
		}

		public virtual async Task<HandlerResult<Success, IErrorResult>> ModifyGameType(long id, GameRequest gameRequest)
		{
			return new AccessDeniedError();
		}

		public virtual async Task<HandlerResult<Success,IErrorResult>> DeleteGame(long id)
		{
			return new AccessDeniedError();
		}

		public virtual async Task<HandlerResult<Success, IErrorResult>> CreateGameType(GameRequest gameRequest)
		{
			return new AccessDeniedError();
		}

		public async Task<HandlerResult<SuccessData<List<GameResponse>>, IErrorResult>> GetListOfTypesOfAvailableGames()
		{
			return await _gameTypeServiceProvider.GetListOfTypesOfAvailableGames();
		}
	}
}
