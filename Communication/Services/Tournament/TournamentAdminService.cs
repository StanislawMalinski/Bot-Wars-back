using BotWars.Services;
using BotWars.TournamentData;
using Communication.APIs.DTOs;
using Shared.DataAccess.RepositoryInterfaces;

namespace Communication.Services.Tournament
{
	public class TournamentAdminService : TournamentUnidentifiedPlayerService, ITournamentService
	{

		public TournamentAdminService(TournamentServiceProvider tournamentServiceProvider) : base(tournamentServiceProvider)
		{
		}

		public async Task<ServiceResponse<TournamentDto>> AddTournament(TournamentDto tournament)
		{
			return await _tournamentServiceProvider.AddTournament(tournament);
		}

		public async Task<ServiceResponse<TournamentDto>> DeleteTournament(long id)
		{
			return await _tournamentServiceProvider.DeleteTournament(id);
		}

		public async Task<ServiceResponse<TournamentDto>> RegisterSelfForTournament(long tournamentId, long playerId)
		{
			return await _tournamentServiceProvider.RegisterSelfForTournament(tournamentId, playerId);
		}

		public async Task<ServiceResponse<TournamentDto>> UnregisterSelfForTournament(long tournamentId, long playerId)
		{
			return await _tournamentServiceProvider.UnregisterSelfForTournament(tournamentId, playerId);
		}

		public async Task<ServiceResponse<TournamentDto>> UpdateTournament(long id, TournamentDto tournament)
		{
			return await _tournamentServiceProvider.UpdateTournament(id, tournament);
		}
	}
}
