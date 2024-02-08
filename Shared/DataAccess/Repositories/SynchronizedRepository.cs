using Microsoft.EntityFrameworkCore;
using Shared.DataAccess.Context;
using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.Enumerations;
using Shared.Results;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Shared.DataAccess.Repositories;

public class SynchronizedRepository
{
    private DataContext _dataContext;
    private TaskDataContext _taskDataContext;

    public SynchronizedRepository(DataContext dataContext, TaskDataContext taskDataContext)
    {
        _dataContext = dataContext;
        _taskDataContext = taskDataContext;
    }


    public async Task<HandlerResult<Success, IErrorResult>> SynchronizeTask()
    {
        
        return new Success();
    }
    
    public async Task<HandlerResult<Success, IErrorResult>> SynchronizeTournament()
    {
        var notSynchoronized = await _dataContext.Tournaments.Where(x => x.Synchronized == false).ToListAsync();
        foreach (var tour in notSynchoronized)
        {
            await _taskDataContext.Tasks.AddAsync(new _Task()
            {
                Type = TaskTypes.PlayTournament,
                ScheduledOn = tour.TournamentsDate,
                Synchronized = true
            });
            tour.Synchronized = true;
        }

        await _taskDataContext.SaveChangesAsync();
        await _dataContext.SaveChangesAsync();
        return new Success();
    }
}