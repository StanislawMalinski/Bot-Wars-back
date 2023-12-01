using Communication.Services.Validation;
using Shared.DataAccess.DAO;
using Shared.DataAccess.RepositoryInterfaces;
using Shared.DataAccess.Services.Results;

namespace Communication.Services.GameType
{
	public class GameTypeService : Service<IGameTypeService>
    {

	    private IGameTypeService? _playerTypeService;
	    private string login = "login"; // should be obtained in method call
	    private string key = "key";

	    public GameTypeService(GameTypeAdminService adminInterface,
		    GameTypeIdentifiedPlayerService identifiedPlayerInterface,
		    GameTypeBannedPlayerService bannedPlayerInterface,
		    GameTypeUnidentifiedPlayerService unidentifiedUserInterface,
		    GameTypeBadValidation badValidationInterface,
		    IPlayerValidator validator)
	    : base(adminInterface, identifiedPlayerInterface, bannedPlayerInterface, unidentifiedUserInterface, badValidationInterface, validator)
	    {
	    }

	    public async Task<ServiceResponse<GameDto>> CreateGameType(GameDto gameDto)
        {
			_playerTypeService = Validate(login, key);
			return await _playerTypeService.CreateGameType(gameDto);
        }

        public async Task<ServiceResponse<GameDto>> DeleteGame(long id)
        {
			_playerTypeService = Validate(login, key);
			return await _playerTypeService.DeleteGame(id);
        }

        public async Task<ServiceResponse<List<GameDto>>> GetGameTypes()
        {
			_playerTypeService = Validate(login, key);
			return await _playerTypeService.GetGameTypes();
        }

        public async Task<ServiceResponse<GameDto>> ModifyGameType(long id, GameDto gameDto)
        {
			_playerTypeService = Validate(login, key);
			return await _playerTypeService.ModifyGameType(id, gameDto);
        }
    }
}