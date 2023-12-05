using Communication.Services.Validation;
using Shared.DataAccess.DAO;
using Shared.DataAccess.RepositoryInterfaces;
using Shared.Results;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Communication.Services.GameType
{
	public class GameTypeService : Service<IGameService>
    {

	    private IGameService? _playerTypeService;
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

	    public async Task<HandlerResult<Success,IErrorResult>> CreateGameType(GameDto gameDto)
        {
			_playerTypeService = Validate(login, key);
			return await _playerTypeService.CreateGameType(gameDto);
        }

        public async Task<HandlerResult<Success,IErrorResult>>  DeleteGame(long id)
        {
			_playerTypeService = Validate(login, key);
			return await _playerTypeService.DeleteGame(id);
        }

        public async Task<HandlerResult<SuccessData<List<GameDto>>,IErrorResult>>  GetGameTypes()
        {
			_playerTypeService = Validate(login, key);
			return await _playerTypeService.GetGameTypes();
        }

        public async Task<HandlerResult<Success,IErrorResult>>  ModifyGameType(long id, GameDto gameDto)
        {
			_playerTypeService = Validate(login, key);
			return await _playerTypeService.ModifyGameType(id, gameDto);
        }
    }
}