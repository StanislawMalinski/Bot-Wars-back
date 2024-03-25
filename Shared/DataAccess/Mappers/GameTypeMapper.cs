using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.DTO.Requests;
using Shared.DataAccess.DTO.Responses;

namespace Shared.DataAccess.Mappers
{
    public class GameTypeMapper : IGameTypeMapper
    {

        public Game MapRequestToGame(GameRequest gameRequest)
        {
            return new Game
            {
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
                NumbersOfPlayer = game.NumbersOfPlayer,
                GameFileName = game.GameFile,
                GameInstructions = game.GameInstructions,
                InterfaceDefinition = game.InterfaceDefinition,
                IsAvailableForPlay = game.IsAvailableForPlay,
                LastModification = game.LastModification,
                BotIds = game.Bot?.Select(bot => bot.Id).ToList(),
                MatchesIds = game.Matches?.Select(match => match.Id).ToList(),
                TournamentsIds = game.Tournaments?.Select(tournament => tournament.Id).ToList(),
                FileId = game.FileId,
            };
        }
        
        public GameSimpleResponse MapGameToSimpleResponse(Game game)
        {
            return new GameSimpleResponse
            {
                Id = game.Id,
                NumbersOfPlayer = game.NumbersOfPlayer,
                GameFileName = game.GameFile,
                GameInstructions = game.GameInstructions,
                InterfaceDefinition = game.InterfaceDefinition,
                IsAvailableForPlay = game.IsAvailableForPlay,
                LastModification = game.LastModification,
            };
        }
    }
}