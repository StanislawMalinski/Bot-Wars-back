using Shared.DataAccess.DAO;
using Shared.DataAccess.RepositoryInterfaces;
using Shared.DataAccess.Services.Results;

namespace Communication.Services.Tournament
{
	public class TournamentIdentifiedPlayerService : TournamentUnidentifiedPlayerService, ITournamentService
	{
		public TournamentIdentifiedPlayerService(TournamentServiceProvider tournamentServiceProvider) : base(tournamentServiceProvider)
		{
		}
		public async Task<ServiceResponse<TournamentDto>> RegisterSelfForTournament(long tournamentId, long playerId)
		{
			return await _tournamentServiceProvider.RegisterSelfForTournament(tournamentId, playerId);
		}

		public async Task<ServiceResponse<TournamentDto>> UnregisterSelfForTournament(long tournamentId, long playerId)
		{
			return await _tournamentServiceProvider.UnregisterSelfForTournament(tournamentId, playerId);
		}
	}
}
