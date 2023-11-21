using BotWars.Services;
using Shared.DataAccess.DataBaseEntities;

namespace Shared.DataAccess.RepositoryInterfaces;

public interface IArchivedMatchesService
{
    Task<ServiceResponse<ArchivedMatches>> CreateArchivedMatchesAsync(ArchivedMatches ArchivedMatches);
    Task<ServiceResponse<ArchivedMatches>> DeleteArchivedMatchesAsync(long id);
    Task<ServiceResponse<ArchivedMatches>> GetArchivedMatchesAsync(long id);
    Task<ServiceResponse<List<ArchivedMatches>>> GetArchivedMatchessAsync();
    Task<ServiceResponse<ArchivedMatches>> UpdateArchivedMatchesAsync(ArchivedMatches ArchivedMatches);
}