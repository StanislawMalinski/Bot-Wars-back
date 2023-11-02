namespace BotWars.RockPaperScissorsData
{
    public class RockPaperScissorsDto
    {
        public string PlayerOneName { get; set; }
        public string PlayerTwoName { get; set; } 
        public bool HasPlayerOneMoved { get; set; }
        public bool HasPlayerTwoMoved { get; set; }
        public string? Winner { get; set; }

    }
}
