using Communication.Services.Validation;
using Shared.DataAccess.DTO;
using Shared.DataAccess.DTO.Requests;
using Shared.DataAccess.DTO.Responses;
using Shared.DataAccess.RepositoryInterfaces;
using Shared.Results;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Communication.Services.Tournament
{
	public class TournamentService : Service<ITournamentService>
	{
		private ITournamentService? _tournamentService;
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

		public async Task<HandlerResult<Success, IErrorResult>> AddTournament(TournamentRequest tournamentRequest)
		{
			_tournamentService = Validate(login, key);
			return await _tournamentService.AddTournament(tournamentRequest);
		}

		public async Task<HandlerResult<Success, IErrorResult>> DeleteTournament(long id)
		{
			_tournamentService = Validate(login, key);
			return await _tournamentService.DeleteTournament(id);
		}

		public async Task<HandlerResult<SuccessData<List<TournamentResponse>>, IErrorResult>> GetListOfTournaments()
		{
			_tournamentService = Validate(login, key);
			return await _tournamentService.GetListOfTournaments();
		}

		public async Task<HandlerResult<SuccessData<List<TournamentResponse>>, IErrorResult>> GetListOfTournamentsFiltered(
			TournamentFilterRequest tournamentFilterRequest)
		{
			_tournamentService = Validate(login, key);
			return await _tournamentService.GetListOfTournamentsFiltered(tournamentFilterRequest);
		}

		public async Task<HandlerResult<SuccessData<TournamentResponse>, IErrorResult>> GetTournament(long id)
		{
			_tournamentService = Validate(login, key);
			return await _tournamentService.GetTournament(id);
		}

		public async Task<HandlerResult<Success, IErrorResult>> RegisterSelfForTournament(long tournamentId, long botId)
		{
			_tournamentService = Validate(login, key);
			return await _tournamentService.RegisterSelfForTournament(tournamentId, botId);
		}

		public async Task<HandlerResult<Success, IErrorResult>> UnregisterSelfForTournament(long tournamentId, long botId)
		{
			_tournamentService = Validate(login, key);
			return await _tournamentService.UnregisterSelfForTournament(tournamentId, botId);
		}

		public async Task<HandlerResult<Success, IErrorResult>> UpdateTournament(long id, TournamentRequest tournamentRequest)
		{
			_tournamentService = Validate(login, key);
			return await _tournamentService.UpdateTournament(id, tournamentRequest);
		}
	}
}