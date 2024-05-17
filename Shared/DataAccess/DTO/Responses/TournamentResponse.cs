using Shared.DataAccess.Enumerations;

namespace Shared.DataAccess.DTO.Responses;

public class TournamentResponse
{
    public long Id { get; set; }
    public long CreatorId { get; set; }
    public string CreatorName { get; set; }
    public string TournamentTitle { get; set; }
    public string Description { get; set; }
    public int PlayersLimit { get; set; }
    public DateTime TournamentsDate { get; set; }
    public DateTime PostedDate { get; set; }
    public RankingTypes RankingType { get; set; }
    public string? Constraints { get; set; }
    public string Image { get; set; }
    public bool WasPlayedOut { get; set; }
    public int MemoryLimit { get; set; }
    public int TimeLimit { get; set; }
    public string Status { get; set; }
    
    public List<long>? MatchIds { get; set; }

    public struct BotPlayer(long botId,string userName)
    {
        public long BotId = botId;
        public string UserName = userName;
    }
    public List<BotPlayer>? PlayersBots  { get; set; }
   
}