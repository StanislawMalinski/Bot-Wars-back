using Shared.DataAccess.Enumerations;

namespace Shared.DataAccess.DataBaseEntities
{
    public class Tournament
    {
        public long Id { get; set; }
        public string TournamentTitle { get; set; }
        public string Description { get; set; }
        public long GameId { get; set; }
        public Game? Game { get; set; }
        public int PlayersLimit { get; set; }
        public DateTime TournamentsDate { get; set; }
        public DateTime PostedDate { get; set; }
        public TournamentStatus Status { get; set; }
        public RankingTypes RankingType { get; set; }
        public string? Constraints { get; set; }
        public byte[]? Image { get; set; }
        public Player Creator { get; set; }
        public long CreatorId { get; set; }

        //public ArchivedMatchPlayers? ArchivedMatchPlayers { get; set; }
        public List<Matches>? Matches { get; set; }
        public List<TournamentReference>? TournamentReference { get; set; }
        public List<PointHistory>? PointHistories { get; set; }

      
    }
}
