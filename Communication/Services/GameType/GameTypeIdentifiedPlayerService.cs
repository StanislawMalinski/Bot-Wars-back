using Shared.DataAccess.DAO;
using Shared.DataAccess.RepositoryInterfaces;
using Shared.DataAccess.Services.Results;

namespace Communication.Services.GameType
{
	public class GameTypeIdentifiedPlayerService : GameTypeUnidentifiedPlayerService, IGameTypeService
	{
		public GameTypeIdentifiedPlayerService(GameTypeServiceProvider gameTypeServiceProvider) : base(gameTypeServiceProvider)
		{ 
		}

		public override async Task<ServiceResponse<GameDto>> CreateGameType(GameDto game)
		{
			return await _gameTypeServiceProvider.addGameType(game);
		}
	}
}
