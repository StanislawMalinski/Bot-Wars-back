using BotWars.Services;
using Shared.DataAccess.DataBaseEntities;

namespace Shared.DataAccess.RepositoryInterfaces
{
    public interface IPlayerService
    {
        public Task<ServiceResponse<Player>> CreatePlayerAsync(Player player);
        public Task<ServiceResponse<Player>> DeletePlayerAsync(long id);
        public Task<ServiceResponse<Player>> GetPlayerAsync(long id);
        public Task<ServiceResponse<List<Player>>> GetPlayersAsync();
        public Task<ServiceResponse<Player>> UpdatePlayerAsync(Player player);

    }
}
