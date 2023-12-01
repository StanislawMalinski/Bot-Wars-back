using Shared.DataAccess.DAO;
using Shared.DataAccess.RepositoryInterfaces;
using Shared.DataAccess.Services.Results;

namespace Communication.Services.GameType
{
	public class GameTypeAdminService : GameTypeUnidentifiedPlayerService, IGameTypeService
	{
		public GameTypeAdminService(GameTypeServiceProvider gameTypeServiceProvider) : base(gameTypeServiceProvider)
		{
		}

		public override async Task<ServiceResponse<GameDto>> CreateGameType(GameDto game)
		{
			return await _gameTypeServiceProvider.addGameType(game);
		}

		public override async Task<ServiceResponse<GameDto>> DeleteGame(long id)
		{
			return await _gameTypeServiceProvider.deleteGameType(id);
		}

		public override async Task<ServiceResponse<GameDto>> ModifyGameType(long id, GameDto gameDto)
		{
			return await _gameTypeServiceProvider.updateGameType(id, gameDto);
		}
	}
}
