using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.Services.Results;

namespace Shared.DataAccess.RepositoryInterfaces;

public interface IArchivedMatchesService
{
    Task<ServiceResponse<ArchivedMatches>> CreateArchivedMatchesAsync(ArchivedMatches ArchivedMatches);
    Task<ServiceResponse<ArchivedMatches>> DeleteArchivedMatchesAsync(long id);
    Task<ServiceResponse<ArchivedMatches>> GetArchivedMatchesAsync(long id);
    Task<ServiceResponse<List<ArchivedMatches>>> GetArchivedMatchessAsync();
    Task<ServiceResponse<ArchivedMatches>> UpdateArchivedMatchesAsync(ArchivedMatches ArchivedMatches);
}