using Communication.Services.Validation;
using Shared.DataAccess.DTO;
using Shared.DataAccess.DTO.Requests;
using Shared.DataAccess.DTO.Responses;
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

	    public async Task<HandlerResult<Success,IErrorResult>> CreateGameType(GameRequest gameRequest)
        {
			_gameService = Validate(login, key);
			return await _gameService.CreateGameType(gameRequest);
        }

        public async Task<HandlerResult<Success,IErrorResult>>  DeleteGame(long id)
        {
			_gameService = Validate(login, key);
			return await _gameService.DeleteGame(id);
        }

        public async Task<HandlerResult<SuccessData<List<GameResponse>>,IErrorResult>> GetGameTypes()
        {
			_gameService = Validate(login, key);
			return await _gameService.GetGames();
        }

        public async Task<HandlerResult<Success,IErrorResult>>  ModifyGameType(long id, GameRequest gameRequest)
        {
			_gameService = Validate(login, key);
			return await _gameService.ModifyGameType(id, gameRequest);
        }
        
        public async Task<HandlerResult<SuccessData<GameResponse>,IErrorResult>> GetGame(long id)
        {
	        _gameService = Validate(login, key);
	        return await _gameService.GetGame(id);
        }

        public async Task<HandlerResult<SuccessData<List<GameResponse>>,IErrorResult>> GetAvailableGames()
        {
	        _gameService = Validate(login, key);
	        return await _gameService.GetListOfTypesOfAvailableGames();
        }
        
    }
}