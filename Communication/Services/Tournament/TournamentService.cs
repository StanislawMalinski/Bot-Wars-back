using Communication.Services.Validation;
using Shared.DataAccess.DAO;
using Shared.DataAccess.RepositoryInterfaces;
using Shared.Results;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Communication.Services.Tournament
{
	public class TournamentService : Service<ITournamentService>
	{
		private ITournamentService? _playerTypeService;
		private string login = "login"; // should be obtained in method call
		private string key = "key";

		public TournamentService(TournamentAdminService adminInterface,
			TournamentIdentifiedPlayerService identifiedPlayerInterface,
			TournamentBannedPlayerService bannedPlayerInterface,
			TournamentUnidentifiedPlayerService unidentifiedUserInterface,
			TournamentBadValidation badValidationInterface,
			IPlayerValidator validator)
		: base(adminInterface, identifiedPlayerInterface, bannedPlayerInterface, unidentifiedUserInterface, badValidationInterface, validator)
		{
		}

		public async Task<HandlerResult<Success, IErrorResult>> AddTournament(TournamentDto tournament)
		{
			_playerTypeService = Validate(login, key);
			return await _playerTypeService.AddTournament(tournament);
		}

		public async Task<HandlerResult<Success, IErrorResult>> DeleteTournament(long id)
		{
			_playerTypeService = Validate(login, key);
			return await _playerTypeService.DeleteTournament(id);
		}

		public async Task<HandlerResult<SuccessData<List<TournamentDto>>, IErrorResult>> GetListOfTournaments()
		{
			_playerTypeService = Validate(login, key);
			return await _playerTypeService.GetListOfTournaments();
		}

		public async Task<HandlerResult<SuccessData<List<TournamentDto>>, IErrorResult>> GetListOfTournamentsFiltered()
		{
			_playerTypeService = Validate(login, key);
			return await _playerTypeService.GetListOfTournamentsFiltered();
		}

		public async Task<HandlerResult<SuccessData<TournamentDto>, IErrorResult>> GetTournament(long id)
		{
			_playerTypeService = Validate(login, key);
			return await _playerTypeService.GetTournament(id);
		}

		public async Task<HandlerResult<Success, IErrorResult>> RegisterSelfForTournament(long tournamentId, long playerId)
		{
			_playerTypeService = Validate(login, key);
			return await _playerTypeService.RegisterSelfForTournament(tournamentId, playerId);
		}

		public async Task<HandlerResult<Success, IErrorResult>> UnregisterSelfForTournament(long tournamentId, long playerId)
		{
			_playerTypeService = Validate(login, key);
			return await _playerTypeService.UnregisterSelfForTournament(tournamentId, playerId);
		}

		public async Task<HandlerResult<Success, IErrorResult>> UpdateTournament(long id, TournamentDto tournament)
		{
			_playerTypeService = Validate(login, key);
			return await _playerTypeService.UpdateTournament(id, tournament);
		}
	}
}