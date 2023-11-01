namespace BotWars.RockPaperScissorsData
{
    public class RockPaperScissorsMapper : IRockPaperScissorsMapper
    {
        public RockPaperScissorsMapper() { }

        public RockPaperScissorsDto toDto(RockPaperScissors rps)
        {
            bool HasPlayerOneMoved = false;
            bool HasPlayerTwoMoved = false;
            Console.WriteLine(rps.SymbolPlayerOne);
            if (!rps.SymbolPlayerOne.Equals(Symbol.NONE))
            {
                HasPlayerOneMoved = true;
            }
            if (!rps.SymbolPlayerTwo.Equals(Symbol.NONE))
            {
               
                HasPlayerTwoMoved = true;
            }
            return new RockPaperScissorsDto
            {
                PlayerOneName = rps.PlayerOneName,
                PlayerTwoName = rps.PlayerTwoName,
                HasPlayerOneMoved = HasPlayerOneMoved,
                HasPlayerTwoMoved = HasPlayerTwoMoved,
                Winner = rps.Winner
            };
        }
    }
}
