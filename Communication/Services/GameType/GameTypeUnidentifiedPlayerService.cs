using Shared.DataAccess.DAO;
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

		public virtual async Task<HandlerResult<Success,IErrorResult>> CreateGameType(GameDto game)
		{
			return new AccessDeniedError();
		}

		public virtual async Task<HandlerResult<SuccessData<List<GameDto>>, IErrorResult>> GetGameTypes()
		{
			return await _gameTypeServiceProvider.getListOfTypesOfGames();
		}

		public virtual async Task<HandlerResult<Success, IErrorResult>> ModifyGameType(long id, GameDto gameDto)
		{
			return new AccessDeniedError();
		}

		public virtual async Task<HandlerResult<Success,IErrorResult>> DeleteGame(long id)
		{
			return new AccessDeniedError();
		}

		

	}
}
