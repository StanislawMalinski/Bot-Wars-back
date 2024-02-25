using System.Data;
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

public class TaskRepository
{
    private DataContext _taskDataContext;

    public TaskRepository(DataContext taskDataContext)
    {
        _taskDataContext = taskDataContext;
    }

    public async Task<HandlerResult<SuccessData<long>, IErrorResult>> CreateTask(TaskTypes type, long operatingOn,DateTime scheduledOn , TaskStatus status = TaskStatus.ToDo)
    {
        _Task task = new _Task
        {
            Type = type,
            OperatingOn = operatingOn,
            ScheduledOn = scheduledOn,
            Status = status
        };
        var res = await _taskDataContext.AddAsync(task);
        await _taskDataContext.SaveChangesAsync();
        return new SuccessData<long>()
        {
            Data = res.Entity.Id
        };
    }
    
    
    
    public async Task<HandlerResult<SuccessData<TaskStatus>, IErrorResult>> CheckStatus(TaskTypes type, long operatingOn)
    {
        var res = await _taskDataContext.Tasks.FirstOrDefaultAsync(x => x.Type == type && x.OperatingOn == operatingOn);
        if (res == null) return new EntityNotFoundErrorResult();
        return new SuccessData<TaskStatus>()
        {
            Data = res.Status
        };
    }
    
    public async Task<HandlerResult<SuccessData<_Task>, IErrorResult>> StartTask(long taskId)
    {
        var res = await _taskDataContext.Tasks.FirstOrDefaultAsync(x => x.Id == taskId);
        if (res == null) return new EntityNotFoundErrorResult();
        return new SuccessData<_Task>()
        {
            Data = res
        };
    }
    
    public async Task<HandlerResult<Success, IErrorResult>> TaskComplete(long taskId)
    {
        var res = await _taskDataContext.Tasks.FirstOrDefaultAsync(x => x.Id == taskId);
        if (res == null) return new EntityNotFoundErrorResult();
        res.Status = TaskStatus.Done;
        await _taskDataContext.SaveChangesAsync();
        return new Success();
    }
    
}