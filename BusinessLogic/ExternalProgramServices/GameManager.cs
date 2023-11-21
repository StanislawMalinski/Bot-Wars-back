using BotWars.Models;
using BotWars.Services;
using BotWars.Services.Constants;

namespace BusinessLogic.ExternalProgramServices
{
    public class GameManager : IGameManager
    {
        private GameResult _gameResult;
        private readonly int GameID = 0;
        private int activeId;
        private ProgramInstance _game;
        private List<ProgramInstance> _bots;
        public void PlayGame(ProgramInstance game, List<ProgramInstance> bots)
        {
            _game = game;
            _bots = bots;
            activeId = GameID;
            int id = 1;
            foreach (ProgramInstance bot in _bots)
            {
                bot.Run(id++);
            }
            _game.Run(0);
        }

        public void HandleOutput(int id, string output)
        {
            if (id != activeId) // ? ignore if not active ?
            {
                return; 
            }
            if (id == GameID)
            {
                HandleGameOutput(output);
            }
            else
            {
                HandleBotOutput(id, output);
            }
        }

        private bool ValidateBotID(int id)
        {
            return id <= _bots.Count;
        }

        private void HandleBotOutput(int id, string output) 
        {
            var match = MessageFormats.MatchBotToGameMessage(output);
            if (!match.Success)
            {
                // disqualify bot
                OnGameEnded();
                return;
            }
            activeId = GameID;
            SendData(GameID, output);
        }

        private void SendData(int id, string data)
        {
            if (id == GameID)
            {
                _game.SendData(data);
            }
            else
            {
                _bots[id].SendData(data);
            }

        }

        private void HandleGameOutput(string output) 
        {
            var match = MessageFormats.MatchGameEnded(output);
            if (match.Success)
            {
                OnGameEnded();
                return;
            }
            var m = MessageFormats.MatchGameToBotMessage(output);
            if (m.Success && ValidateBotID(m.Data.Id)) 
            {
                activeId = m.Data.Id;
                SendData(m.Data.Id, m.Data.Data);
            }
        }

        public void HandleExit(int id) 
        {
            // check which process exited and update game result
            OnGameEnded();
        }
        private void OnGameEnded()
        {
            // Close processes
        }
    }
}
