using System.Runtime.ConstrainedExecution;

namespace BotWars.Services.Constants
{
    public class RockPaperScissorsConstants
    {
        public const string TIE = "Tie";
        public const string TIE_MESSAGE = "The game has ended with a tie!";
        public const string VICTORY_MESSAGE = "Winner of the game is: ";
        public const string GAME_ONGOING_MESSAGE = "Not everyone has made their move";
        public const string GAME_CREATED_SUCCESS = "A game of rock paper scissors has been successfully created! Player one can make their move.";
        public const string GAME_CREATED_FAILURE = "A game of rock paper scissors could not have been created";
        public const string DATABASE_FAILURE = "Problem with database";
        public const string PLAYER_ONE_MOVED = "Player one has already made their move!";
        public const string PLAYER_TWO_MOVED = "Player two has already made their move!";
        public const string DELETED_MESSAGE = "A game of rock paper scissors has been deleted";
        public const string NO_GAMES_FOUND = "There are no rock paper scissors games in database";
    }
}
