using BotWars.Games;

namespace BotWars.Services;

public interface IPlayerService
{
    Task<ServiceResponse<Player>> CreatePlayerAsync(Player Player);
    Task<ServiceResponse<Player>> DeletePlayerAsync(long id);
    Task<ServiceResponse<Player>> GetPlayerAsync(long id);
    Task<ServiceResponse<List<Player>>> GetPlayersAsync();
    Task<ServiceResponse<Player>> UpdatePlayerAsync(Player Player);
}