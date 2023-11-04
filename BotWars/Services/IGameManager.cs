namespace BotWars.Services
{
    public interface IGameManager
    {
        // rather useless interface
        public void PlayGame(ProgramInstance game, List<ProgramInstance> bots);
        public void HandleOutput(int processId, string output);
    }
}
