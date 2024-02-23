using Shared.DataAccess.DTO;
using Shared.DataAccess.RepositoryInterfaces;
using Shared.Results;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Communication.Services.Tournament
{
	public class TournamentAdminService : TournamentUnidentifiedPlayerService, ITournamentService
	{

		public TournamentAdminService(TournamentServiceProvider tournamentServiceProvider) : base(tournamentServiceProvider)
		{
		}

		public async Task<HandlerResult<Success, IErrorResult>> AddTournament(TournamentDto tournament)
		{
			return await _tournamentServiceProvider.AddTournament(tournament);
		}

		public async Task<HandlerResult<Success, IErrorResult>> DeleteTournament(long id)
		{
			return await _tournamentServiceProvider.DeleteTournament(id);
		}

		public async Task<HandlerResult<Success, IErrorResult>> RegisterSelfForTournament(long tournamentId, long botId)
		{
			return await _tournamentServiceProvider.RegisterSelfForTournament(tournamentId, botId);
		}

		public async Task<HandlerResult<Success, IErrorResult>> UnregisterSelfForTournament(long tournamentId, long botId)
		{
			return await _tournamentServiceProvider.UnregisterSelfForTournament(tournamentId, botId);
		}

		public async Task<HandlerResult<Success, IErrorResult>> UpdateTournament(long id, TournamentDto tournament)
		{
			return await _tournamentServiceProvider.UpdateTournament(id, tournament);
		}
	}
}
