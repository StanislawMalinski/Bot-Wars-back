using Shared.DataAccess.DTO;
using Shared.DataAccess.RepositoryInterfaces;
using Shared.Results;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Communication.Services.GameType
{
	public class GameTypeAdminService : GameTypeUnidentifiedPlayerService, IGameService
	{
		public GameTypeAdminService(GameTypeServiceProvider gameTypeServiceProvider) : base(gameTypeServiceProvider)
		{
		}

		public override async Task<HandlerResult<Success,IErrorResult>>  CreateGameType(GameDto game)
		{
			return await _gameTypeServiceProvider.addGameType(game);
		}

		public override async Task<HandlerResult<Success,IErrorResult>>  DeleteGame(long id)
		{
			return await _gameTypeServiceProvider.deleteGameType(id);
		}

		public override async Task<HandlerResult<Success,IErrorResult>>  ModifyGameType(long id, GameDto gameDto)
		{
			return await _gameTypeServiceProvider.updateGameType(id, gameDto);
		}
	}
}
