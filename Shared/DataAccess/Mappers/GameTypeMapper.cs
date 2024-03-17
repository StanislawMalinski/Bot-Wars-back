using Shared.DataAccess.DAO;
using Shared.DataAccess.DTO;
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
                IsAvaiableForPlay = game.IsAvailableForPlay,
                LastModification = game.LastModification
            };
            

        }

        public Game ToGameType(GameDto game)
        {
            return new Game
            {
<<<<<<< HEAD
                NumbersOfPlayer = gameRequest.NumberOfPlayer,
                GameFile = gameRequest.GameFile?.FileName,
                GameInstructions = gameRequest.GameInstructions,
                InterfaceDefinition = gameRequest.InterfaceDefinition,
                IsAvailableForPlay = gameRequest.IsAvailableForPlay,
                LastModification = DateTime.Now,
                Bot = new List<Bot>(),
                Tournaments = new List<Tournament>(),
                Matches = new List<Matches>(),
            };
        }

        public GameResponse MapGameToResponse(Game game)
        {
            return new GameResponse
            {
                Id = game.Id,
=======
>>>>>>> parent of 82c6fa8 (Merge pull request #91 from StanislawMalinski/eloHell)
                NumbersOfPlayer = game.NumbersOfPlayer,
                GameFile = game.GameFile,
                GameInstructions = game.GameInstructions,
                InterfaceDefinition = game.InterfaceDefinition,
<<<<<<< HEAD
                IsAvailableForPlay = game.IsAvailableForPlay,
                LastModification = game.LastModification,
                BotIds = game.Bot?.Select(bot => bot.Id).ToList(),
                MatchesIds = game.Matches?.Select(match => match.Id).ToList(),
                TournamentsIds = game.Tournaments?.Select(tournament => tournament.Id).ToList(),
                FileId = game.FileId,
=======
                IsAvailableForPlay = game.IsAvaiableForPlay,
                LastModification = game.LastModification
>>>>>>> parent of 82c6fa8 (Merge pull request #91 from StanislawMalinski/eloHell)
            };
          

        }

    }
}
