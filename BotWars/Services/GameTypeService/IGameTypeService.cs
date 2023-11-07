using BotWars.GameTypeData;

namespace BotWars.Services.GameTypeService
{
    public interface IGameTypeService
    {
        public Task<ServiceResponse<GameTypeDto>> CreateGameType(GameType gameType);
        public Task<ServiceResponse<List<GameTypeDto>>> GetGameTypes();
        public Task<ServiceResponse<GameTypeDto>> DeleteGame(long id);
    }
}
