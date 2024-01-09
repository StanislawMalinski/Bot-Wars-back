﻿namespace Shared.DataAccess.DataBaseEntities
{
    public class Player
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public bool isBanned { get; set; }
        public long Points { get; set; }
        public int RoleId { get; set; }
        public  Role Role { get; set; }
        
        public List<Bot>? Bot { get; set; }
        public UserSettings UserSettings { get; set; }
        public PlayerPassword PlayerPassword { get; set; }
        public List<MatchPlayers>? MatchPlayers { get; set; }
        public List<PointHistory>? PlayerPointsList { get; set; }
        public List<AchievementRecord> AchievementRecords { get; set; }
    }
}
