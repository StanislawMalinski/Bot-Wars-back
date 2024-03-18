namespace Shared.DataAccess.DataBaseEntities
{
    public class Player
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public bool isBanned { get; set; }
        public long Points { get; set; }
        public string? HashedPassword { get; set; }
        public int RoleId { get; set; }
        public DateTime Registered { get; set; }
        public DateTime LastLogin { get; set; }
        public  Role Role { get; set; }
        
        public List<Bot>? Bot { get; set; }
        public UserSettings UserSettings { get; set; }
        //public List<MatchPlayers>? MatchPlayers { get; set; }
        public List<PointHistory>? PlayerPointsList { get; set; }
        public List<AchievementRecord> AchievementRecords { get; set; }
        public List<NotificationOutbox> NotificationOutboxes { get; set; }
        public List<Tournament> Tournaments { get; set; }
    }
}
