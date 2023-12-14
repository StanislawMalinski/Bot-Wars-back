using Shared.DataAccess.Context;
using Shared.DataAccess.DAO;
using Shared.DataAccess.RepositoryInterfaces;
using Shared.Results;
using Shared.Results.ErrorResults;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Shared.DataAccess.Repositories;

public class AdministrativeRepository : IAdministrativeRepository
{
    private readonly DataContext _dataContext;

    public AdministrativeRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }
    public async Task<HandlerResult<Success, IErrorResult>> UnbanPlayer(long playerId)
    {
        var resPlayer = await _dataContext.Players.FindAsync(playerId);
        if (resPlayer == null)
        {
            return new EntityNotFoundErrorResult()
            {
                Title = "return null",
                Message = "Nie znaleziono gracza w bazie danych"
            };
        }
        resPlayer.isBanned = false;
        await _dataContext.SaveChangesAsync();
        return new Success();
    }

    public async Task<HandlerResult<Success, IErrorResult>> BanPlayer(long playerId)
    {
        var resPlayer = await _dataContext.Players.FindAsync(playerId);
        if (resPlayer == null)
        {
            return new EntityNotFoundErrorResult()
            {
                Title = "return null",
                Message = "Nie znaleziono gracza w bazie danych"
            };
        }

        resPlayer.isBanned = true;
        await _dataContext.SaveChangesAsync();
        return new Success();
    }
}