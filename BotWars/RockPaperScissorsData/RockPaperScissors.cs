namespace BotWars.RockPaperScissorsData
{
    public class RockPaperScissors
    {
        public long Id { get; set; }
        public string PlayerOneName { get; set; }
        public string PlayerTwoName { get; set; }
        public Symbol SymbolPlayerOne { get; set; }
        public Symbol SymbolPlayerTwo { get; set; }
        public string? Winner {  get; set; }

    }
}
