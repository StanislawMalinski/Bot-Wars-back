using BotWars.Services;
using Communication.APIs.DTOs;
using Shared.DataAccess.RepositoryInterfaces;

namespace Communication.Services.Tournament
{
	public class TournamentUnidentifiedPlayerService : ITournamentService
	{
		// probably TournamentServiceProvider should to be injected as dependency
		protected readonly TournamentServiceProvider _tournamentServiceProvider;

		public TournamentUnidentifiedPlayerService(TournamentServiceProvider tournamentServiceProvider)
		{
			_tournamentServiceProvider = tournamentServiceProvider;
		}

		public async Task<ServiceResponse<TournamentDto>> AddTournament(TournamentDto tournament)
		{
			return ServiceResponse<TournamentDto>.AccessDeniedResponse();
		}

		public async Task<ServiceResponse<TournamentDto>> DeleteTournament(long id)
		{
			return ServiceResponse<TournamentDto>.AccessDeniedResponse();
		}

		public async Task<ServiceResponse<List<TournamentDto>>> GetListOfTournaments()
		{
			return await _tournamentServiceProvider.GetListOfTournaments();
		}

		public async Task<ServiceResponse<List<TournamentDto>>> GetListOfTournamentsFiltered()
		{
			return await _tournamentServiceProvider.GetListOfTournamentsFiltered();
		}

		public async Task<ServiceResponse<TournamentDto>> GetTournament(long id)
		{
			return await _tournamentServiceProvider.GetTournament(id);
		}

		public async Task<ServiceResponse<TournamentDto>> RegisterSelfForTournament(long tournamentId, long playerId)
		{
			return ServiceResponse<TournamentDto>.AccessDeniedResponse();
		}

		public async Task<ServiceResponse<TournamentDto>> UnregisterSelfForTournament(long tournamentId, long playerId)
		{
			return ServiceResponse<TournamentDto>.AccessDeniedResponse();
		}

		public async Task<ServiceResponse<TournamentDto>> UpdateTournament(long id, TournamentDto tournament)
		{
			return ServiceResponse<TournamentDto>.AccessDeniedResponse();
		}
	}
}
