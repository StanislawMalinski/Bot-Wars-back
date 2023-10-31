using BotWars.Gry;

namespace BotWars.Services
{
    public interface IGameServis
    {
        public  Task<ServiceResponse<List<Game>>> GetGamesAsync();

        public Task<ServiceResponse<Game>> GetGameAsync(long id);

        public Task<ServiceResponse<Game>> UpdateGameAsync(Game product);

        public Task<ServiceResponse<Game>> DeleteGameAsync(long id);

        public Task<ServiceResponse<Game>> CreateGameAsync(Game product);
    }


}
