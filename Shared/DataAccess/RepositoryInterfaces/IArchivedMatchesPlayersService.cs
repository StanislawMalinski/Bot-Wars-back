using BotWars.Services;
using Shared.DataAccess.DataBaseEntities;

namespace Shared.DataAccess.Services;

public interface IArchivedMatchesPlayersService
{
    Task<ServiceResponse<ArchivedMatchPlayers>> CreateArchivedMatchPlayersAsync(ArchivedMatchPlayers ArchivedMatchPlayers);
    Task<ServiceResponse<ArchivedMatchPlayers>> DeleteArchivedMatchPlayersAsync(long id);
    Task<ServiceResponse<ArchivedMatchPlayers>> GetArchivedMatchPlayersAsync(long id);
    Task<ServiceResponse<List<ArchivedMatchPlayers>>> GetArchivedMatchPlayerssAsync();
    Task<ServiceResponse<ArchivedMatchPlayers>> UpdateArchivedMatchPlayersAsync(ArchivedMatchPlayers ArchivedMatchPlayers);
}