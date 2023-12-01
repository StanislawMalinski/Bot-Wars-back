using Shared.DataAccess.DAO;
using Shared.DataAccess.RepositoryInterfaces;
using Shared.DataAccess.Services.Results;

namespace Communication.Services.Tournament
{
	public class TournamentBadValidation : ITournamentService
	{
		public TournamentBadValidation(TournamentServiceProvider tournamentServiceProvider)
		{
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
			return ServiceResponse<List<TournamentDto>>.AccessDeniedResponse();
		}

		public async Task<ServiceResponse<List<TournamentDto>>> GetListOfTournamentsFiltered()
		{
			return ServiceResponse<List<TournamentDto>>.AccessDeniedResponse();
		}

		public async Task<ServiceResponse<TournamentDto>> GetTournament(long id)
		{
			return ServiceResponse<TournamentDto>.AccessDeniedResponse();
		}

		public async Task<ServiceResponse<TournamentDto>> RegisterSelfForTournament(long tournamentId, long playerId)
		{
			return ServiceResponse<TournamentDto>.AccessDeniedResponse();
		}

		public async Task<ServiceResponse<TournamentDto>> UnregisterSelfForTournament(long tournamentId, long playerId)
		{
			return ServiceResponse<TournamentDto>.AccessDeniedResponse();
		}

		public async  Task<ServiceResponse<TournamentDto>> UpdateTournament(long id, TournamentDto tournament)
		{
			return ServiceResponse<TournamentDto>.AccessDeniedResponse();
		}
	}
}
