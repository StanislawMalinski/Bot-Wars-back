﻿namespace Shared.DataAccess.DataBaseEntities
{
    public class Player
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        
        public long Points { get; set; }

        public string HashedPassword { get; set; }
        
        public List<Bot>? Bot { get; set; }
        public List<ArchivedMatchPlayers>? ArchivedMatchPlayers { get; set; }
        public List<PointHistory>? PlayerPointsList { get; set; }
    }
}
