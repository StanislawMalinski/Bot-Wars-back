using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.DTO.Responses;

namespace Shared.DataAccess.Mappers;

public class MatchMapper
{
    public MatchResponse MapEntityToResponse(Matches match)
    {
        MatchResponse matchResponse = new(match.Id, match.GameId, match.Game?
                .GameFile , match.TournamentsId, match.Tournament?.TournamentTitle
            ,match.MatchPlayers.Select(x=>new BotPlayer(x.BotId,x.Bot.BotFile,x.Bot.Player.Login)  ).ToList(),match.Status.ToString(),match.Winner
        , match.Played);

        return matchResponse;
    }
    
    public MatchInTournamentRespond MapEntityToMatcInTournamentResponse(Matches match)
    {
        MatchInTournamentRespond matchResponse = new(match.Id, match.MatchPlayers.Select(x=>new BotPlayer(x.BotId,x.Bot.BotFile,x.Bot.Player.Login)).ToList()
            ,match.Status.ToString(),match.Winner, 
             Int32.Parse( match.Data ) , match.Played,match.MatchResult.ToString());

        return matchResponse;
    }
}