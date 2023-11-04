using BotWars.Models;
using BotWars.Services;
using BotWars.Services.Constants;
using System.Text.RegularExpressions;

namespace BotWars
{
    public class GameManager : IGameManager
    {
        private readonly int GameID = 0;
        private int activeId;
        public void PlayGame(ProgramInstance game, List<ProgramInstance> bots)
        {
            activeId = GameID;
            int id = 1;
            foreach (ProgramInstance bot in bots)
            {
                bot.Run(id++);
            }
            game.Run(0);
        }

        public void HandleOutput(int id, string output)
        {
            if (id != activeId) // ? ignore if not active ?
            {
                return; 
            }
        }

        private void HandleBotOutput(int id, string output) { }
        private void HandleGameOutput(string output) 
        {
            Match match = Regex.Match(output, MessageFormats.GameEnded);
            if (match.Success)
            {
                // TO DO retrive data
                OnGameEnded();
            }
            match = Regex.Match(output, MessageFormats.GameToBotPrompt);
        }

        private void OnGameEnded()
        {

        }
    }
}
