using Shared.DataAccess.DTO;
using Shared.DataAccess.RepositoryInterfaces;
using Shared.Results;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Communication.Services.Tournament
{
	public class TournamentIdentifiedPlayerService : TournamentUnidentifiedPlayerService, ITournamentService
	{
		public TournamentIdentifiedPlayerService(TournamentServiceProvider tournamentServiceProvider) : base(tournamentServiceProvider)
		{
		}
		public async Task<HandlerResult<Success, IErrorResult>> RegisterSelfForTournament(long tournamentId, long botId)
		{
			return await _tournamentServiceProvider.RegisterSelfForTournament(tournamentId, botId);
		}

		public async Task<HandlerResult<Success, IErrorResult>> UnregisterSelfForTournament(long tournamentId, long botId)
		{
			return await _tournamentServiceProvider.UnregisterSelfForTournament(tournamentId, botId);
		}
	}
}
