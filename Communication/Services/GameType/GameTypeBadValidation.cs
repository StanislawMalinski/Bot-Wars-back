using Communication.Services.Validation;
using Shared.DataAccess.DAO;
using Shared.DataAccess.RepositoryInterfaces;
using Shared.DataAccess.Services.Results;

namespace Communication.Services.GameType
{
	public class GameTypeBadValidation : IGameTypeService
	{
		public async Task<ServiceResponse<GameDto>> CreateGameType(GameDto game)
		{
			return ServiceResponse<GameDto>.AccessDeniedResponse();
		}

		public async Task<ServiceResponse<GameDto>> DeleteGame(long id)
		{
			return ServiceResponse<GameDto>.AccessDeniedResponse();
		}

		public async Task<ServiceResponse<List<GameDto>>> GetGameTypes()
		{
			return ServiceResponse<List<GameDto>>.AccessDeniedResponse();
		}

		public async Task<ServiceResponse<GameDto>> ModifyGameType(long id, GameDto gameDto)
		{
			return ServiceResponse<GameDto>.AccessDeniedResponse();
		}
	}
}
