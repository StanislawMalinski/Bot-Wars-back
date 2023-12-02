using Shared.DataAccess.DAO;
using Shared.DataAccess.DataBaseEntities;

namespace Shared.DataAccess.Mappers
{
	public class GameTypeMapper : IGameTypeMapper
    {
        public GameTypeMapper()
        {
            
        }
        public GameDto ToDto(Game game)
        {
            return new GameDto
            {
                NumbersOfPlayer = game.NumbersOfPlayer,
                GameFile = game.GameFile,
                GameInstructions = game.GameInstructions,
                InterfaceDefinition = game.InterfaceDefinition,
                IsAvaiableForPlay = game.IsAvaiableForPlay,
                LastModification = game.LastModification
            };
            

        }

        public Game ToGameType(GameDto game)
        {
            return new Game
            {
                NumbersOfPlayer = game.NumbersOfPlayer,
                GameFile = game.GameFile,
                GameInstructions = game.GameInstructions,
                InterfaceDefinition = game.InterfaceDefinition,
                IsAvaiableForPlay = game.IsAvaiableForPlay,
                LastModification = game.LastModification
            };
          

        }

    }
}
