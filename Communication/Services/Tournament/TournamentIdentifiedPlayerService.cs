using BotWars.Services;
using BotWars.TournamentData;
using Communication.APIs.DTOs;
using Shared.DataAccess.RepositoryInterfaces;

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
