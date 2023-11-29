using BotWars.Services;
using Communication.APIs.DTOs;

namespace Shared.DataAccess.RepositoryInterfaces
{
    public interface IGameTypeService
    {
        public Task<ServiceResponse<GameDto>> CreateGameType(GameDto game);
        public Task<ServiceResponse<List<GameDto>>> GetGameTypes();
        public Task<ServiceResponse<GameDto>> DeleteGame(long id);
        public Task<ServiceResponse<GameDto>> ModifyGameType(long id, GameDto gameDto);

    }
}
