using Shared.DataAccess.RepositoryInterfaces;

namespace Communication.Services.Tournament
{
	public class TournamentBannedPlayerService : TournamentUnidentifiedPlayerService, ITournamentService
	{
		public TournamentBannedPlayerService(TournamentServiceProvider tournamentServiceProvider) : base(tournamentServiceProvider)
		{
		}
	}
}
