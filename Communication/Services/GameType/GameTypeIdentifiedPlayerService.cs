using Shared.DataAccess.DAO;
using Shared.DataAccess.RepositoryInterfaces;
using Shared.Results;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Communication.Services.GameType
{
	public class GameTypeIdentifiedPlayerService : GameTypeUnidentifiedPlayerService, IGameService
	{
		public GameTypeIdentifiedPlayerService(GameTypeServiceProvider gameTypeServiceProvider) : base(gameTypeServiceProvider)
		{ 
		}

		public override async Task<HandlerResult<Success,IErrorResult>>  CreateGameType(GameDto game)
		{
			return await _gameTypeServiceProvider.addGameType(game);
		}
	}
}
