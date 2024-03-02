using Microsoft.EntityFrameworkCore;
using Shared.DataAccess.Context;
using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.Enumerations;
using Shared.Results;
using Shared.Results.ErrorResults;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;
using TaskStatus = Shared.DataAccess.Enumerations.TaskStatus;

namespace Shared.DataAccess.Repositories;

public class SchedulerRepository
{
    private DataContext _dataContext;

    public SchedulerRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<HandlerResult<SuccessData<List<_Task>>, IErrorResult>> TaskToDo()
    {

        DateTime data = DateTime.Now;
        var result = await _dataContext.Tasks.Where(x => x.ScheduledOn <= data && x.Status == TaskStatus.ToDo).ToListAsync();
        //var result = await _dataContext.Tasks.Where(x => x.ScheduledOn < data && x.Status == false).ToListAsync();
        Console.WriteLine(result.Count() + " tasks");
        return new SuccessData<List<_Task>>()
        {
            Data = result
        };
    }

    public async Task<HandlerResult<SuccessData<List<_Task>>, IErrorResult>> TaskToDo(int numOfTasks)
    {
        DateTime data = DateTime.Now;
        List<_Task> result = new List<_Task>();
        var tournaments = await _dataContext.Tasks
            .Where(x => x.Type == TaskTypes.PlayTournament && x.Status == TaskStatus.ToDo)
            .ToListAsync();
        foreach (_Task t in tournaments)
        {
            if (result.Count >= numOfTasks)
            {
                return new SuccessData<List<_Task>>()
                {
                    Data = result
                };
            }
            result.Add(t);
        }
        var matches = await _dataContext.Tasks
            .Where(x => x.Type == TaskTypes.PlayGame && x.Status == TaskStatus.ToDo)
            .ToListAsync();
        foreach (_Task t in tournaments)
        {
            if (result.Count >= numOfTasks)
            {
                return new SuccessData<List<_Task>>()
                {
                    Data = result
                };
            }
            result.Add(t);
        }
        var tournamentsInProgress = await _dataContext.Tasks
            .Where(x => x.Type == TaskTypes.PlayTournament && x.Status == TaskStatus.Doing)
            .ToListAsync();
        foreach (_Task t in tournamentsInProgress)
        {
            if (result.Count >= numOfTasks)
            {
                return new SuccessData<List<_Task>>()
                {
                    Data = result
                };
            }
            result.Add(t);
        }
        var others = await _dataContext.Tasks
            .Where(x => x.Type != TaskTypes.PlayTournament && x.Type != TaskTypes.PlayGame && x.ScheduledOn <= data && x.Status == TaskStatus.ToDo)
            .OrderBy(x => x.ScheduledOn)
            .ToListAsync();
        foreach (_Task t in others)
        {
            if (result.Count >= numOfTasks)
            {
                return new SuccessData<List<_Task>>()
                {
                    Data = result
                };
            }
            result.Add(t);
        }
        Console.WriteLine("tasks not found");
        return new SuccessData<List<_Task>>()
        {
            Data = result
        };
    }

    public async Task<HandlerResult<Success, IErrorResult>> Taskdoing(long taskId)
    {
        var res = await _dataContext.Tasks.FirstOrDefaultAsync(x => x.Id == taskId);
        if (res == null)
        {
            return new EntityNotFoundErrorResult();
        }
        res.Status = TaskStatus.Doing;
        await _dataContext.SaveChangesAsync();
        return new Success();
    }
}