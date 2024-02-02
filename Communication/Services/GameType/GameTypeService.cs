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

	    private IGameService? _gameService;
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
			_gameService = Validate(login, key);
			return await _gameService.CreateGameType(gameDto);
        }

        public async Task<HandlerResult<Success,IErrorResult>>  DeleteGame(long id)
        {
			_gameService = Validate(login, key);
			return await _gameService.DeleteGame(id);
        }

        public async Task<HandlerResult<SuccessData<List<GameDto>>,IErrorResult>>  GetGameTypes()
        {
			_gameService = Validate(login, key);
			return await _gameService.GetGameTypes();
        }

        public async Task<HandlerResult<Success,IErrorResult>>  ModifyGameType(long id, GameDto gameDto)
        {
			_gameService = Validate(login, key);
			return await _gameService.ModifyGameType(id, gameDto);
        }
    }
}