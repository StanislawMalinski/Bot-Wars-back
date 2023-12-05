using Shared.DataAccess.RepositoryInterfaces;

namespace Communication.Services.GameType
{
	public class GameTypeBannedPlayerService : GameTypeUnidentifiedPlayerService, IGameService
	{
		public GameTypeBannedPlayerService(GameTypeServiceProvider gameTypeServiceProvider) : base(gameTypeServiceProvider)
		{
		}
	}
}
