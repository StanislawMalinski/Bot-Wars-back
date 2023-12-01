using Shared.DataAccess.RepositoryInterfaces;

namespace Communication.Services.GameType
{
	public class GameTypeBannedPlayerService : GameTypeUnidentifiedPlayerService, IGameTypeService
	{
		public GameTypeBannedPlayerService(GameTypeServiceProvider gameTypeServiceProvider) : base(gameTypeServiceProvider)
		{
		}
	}
}
