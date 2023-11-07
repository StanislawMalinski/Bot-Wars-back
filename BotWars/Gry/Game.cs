﻿namespace BotWars.Gry
{
    public class Game
    {
        public long Id { get; set; }
        public int NumbersOfPlayer { get; set; }
        public DateTime LastModification { get; set; }
        public String? GameFile { get; set; }
        public String? GameInstructions { get; set; }
        public String? InterfaceDefinition { get; set; }
        public bool IsAvaiableForPlay { get; set; }


        public Bot Bot { get; set; }
        public List<Tournament>? Tournaments { get; set; }
        public List<ArchivedMatches>? ArchivedMatches { get; set; }

        

    }
}
