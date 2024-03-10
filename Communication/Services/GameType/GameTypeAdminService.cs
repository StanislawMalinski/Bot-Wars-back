using Shared.DataAccess.DTO.Requests;
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

		public override async Task<HandlerResult<Success,IErrorResult>>  CreateGameType(GameRequest gameRequest)
		{
			return await _gameTypeServiceProvider.AddGameType(gameRequest);
		}

		public override async Task<HandlerResult<Success,IErrorResult>>  DeleteGame(long id)
		{
			return await _gameTypeServiceProvider.DeleteGameType(id);
		}

		public override async Task<HandlerResult<Success,IErrorResult>>  ModifyGameType(long id, GameRequest gameRequest)
		{
			return await _gameTypeServiceProvider.UpdateGameType(id, gameRequest);
		}
	}
}
