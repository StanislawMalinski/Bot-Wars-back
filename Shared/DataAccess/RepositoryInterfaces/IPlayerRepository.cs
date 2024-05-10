using Microsoft.EntityFrameworkCore.ChangeTracking;
using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.DTO;
using Shared.DataAccess.DTO.Requests;
using Shared.DataAccess.DTO.Responses;
using Shared.Results;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Shared.DataAccess.RepositoryInterfaces;

public interface IPlayerRepository
{
    
    
   
    Task<List<GameSimpleResponse>> GetMyGames(long playerId);
    Task<bool> DeletePlayerAsync(long id);
    Task<bool> SetPlayerLastLogin(string email, DateTime lastLogin);
    Task<Player?> GetPlayer(long playerId);
    Task<Player?> GetPlayer(string playerEmail);
    Task<Player?> GetPlayerByLogin(string login);
    Task<int> PlayerBotCount(long playerId);
    Task<int> PlayerTournamentCount(long playerId);
    Task SaveChangesAsync();
    Task<EntityEntry<Player>> AddPlayer(Player player);
    Task<List<BotResponse>> GetPlayerBots(long playerId);
}