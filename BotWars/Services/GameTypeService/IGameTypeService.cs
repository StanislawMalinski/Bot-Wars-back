using BotWars.GameTypeData;

namespace BotWars.Services.GameTypeService
{
    public interface IGameTypeService
    {
        public Task<ServiceResponse<GameTypeDto>> CreateGameType(GameTypeDto gameType);
        public Task<ServiceResponse<List<GameTypeDto>>> GetGameTypes();
        public Task<ServiceResponse<GameTypeDto>> DeleteGame(long id);
        public Task<ServiceResponse<GameTypeDto>> ModifyGameType(long id, GameTypeDto gameTypeDto);

    }
}
