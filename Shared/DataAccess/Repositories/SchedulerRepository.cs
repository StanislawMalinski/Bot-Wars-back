using Microsoft.EntityFrameworkCore;
using Shared.DataAccess.Context;
using Shared.DataAccess.DataBaseEntities;
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
        Console.WriteLine(result.Count() + " tle zadan");
        return new SuccessData<List<_Task>>()
        {
            Data = result
        };
    }

    public async Task<HandlerResult<SuccessData<List<_Task>>, IErrorResult>> TaskToDo(int engineId)
    {

        DateTime data = DateTime.Now;
        var result = await _dataContext.Tasks.Where(x => x.ScheduledOn <= data && x.Status == TaskStatus.ToDo && x.EngineId == engineId).ToListAsync();
        Console.WriteLine(result.Count() + " tasks to do");
        return new SuccessData<List<_Task>>()
        {
            Data = result
        };
    }

    public async Task<HandlerResult<SuccessData<List<_Task>>, IErrorResult>> UnassignedTasks()
    {

        DateTime data = DateTime.Now;
        var result = await _dataContext.Tasks.Where(x => x.ScheduledOn <= data && x.Status == TaskStatus.Unassigned).ToListAsync();
        Console.WriteLine(result.Count() + " unassigned tasks");
        return new SuccessData<List<_Task>>()
        {
            Data = result
        };
    }

    public async Task<HandlerResult<Success, IErrorResult>> AssignTask(long taskId, int engineId)
    {
        var res = await _dataContext.Tasks.FirstOrDefaultAsync(x => x.Id == taskId);
        if (res == null || res.Status != TaskStatus.Unassigned)
        {
            return new EntityNotFoundErrorResult();
        }
        res.Status = TaskStatus.ToDo;
        res.EngineId = engineId;
        await _dataContext.SaveChangesAsync();
        return new Success();
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
