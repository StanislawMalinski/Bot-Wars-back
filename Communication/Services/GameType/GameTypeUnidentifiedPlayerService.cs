using Shared.DataAccess.DAO;
using Shared.DataAccess.RepositoryInterfaces;
using Shared.DataAccess.Services.Results;

namespace Communication.Services.GameType
{
	public class GameTypeUnidentifiedPlayerService : IGameTypeService
	{
		protected readonly GameTypeServiceProvider _gameTypeServiceProvider;

		public GameTypeUnidentifiedPlayerService(GameTypeServiceProvider gameTypeServiceProvider)
		{
			_gameTypeServiceProvider = gameTypeServiceProvider;
		}

		public virtual async Task<ServiceResponse<GameDto>> CreateGameType(GameDto game)
		{
			return ServiceResponse<GameDto>.AccessDeniedResponse();
		}

		public virtual async Task<ServiceResponse<GameDto>> DeleteGame(long id)
		{
			return ServiceResponse<GameDto>.AccessDeniedResponse();
		}

		public virtual async Task<ServiceResponse<List<GameDto>>> GetGameTypes()
		{
			return await _gameTypeServiceProvider.getListOfTypesOfGames();
		}

		public virtual async Task<ServiceResponse<GameDto>> ModifyGameType(long id, GameDto gameDto)
		{
			return ServiceResponse<GameDto>.AccessDeniedResponse();
		}
	}
}
