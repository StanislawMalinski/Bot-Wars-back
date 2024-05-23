using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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
    
    
    public async Task<EntityEntry<_Task>> AddTask(TaskTypes type, long operatingOn,DateTime scheduledOn , TaskStatus status = TaskStatus.Unassigned)
    {
        Console.WriteLine("dodano zdanie w repozytoruim");
        _Task task = new _Task
        {
            Type = type,
            OperatingOn = operatingOn,
            ScheduledOn = scheduledOn,
            Status = status
        };
        return await _taskDataContext.AddAsync(task);
    }
    public async Task<EntityEntry<_Task>> AddTask(TaskTypes type, long operatingOn,DateTime scheduledOn ,int enginId, TaskStatus status = TaskStatus.Unassigned)
    {
        
        _Task task = new _Task
        {
            Type = type,
            OperatingOn = operatingOn,
            ScheduledOn = scheduledOn,
            Status = status,
            EngineId = enginId
        };
        return await _taskDataContext.AddAsync(task);
    }
    public async Task<EntityEntry<_Task>> AddTask(_Task task)
    {
        return await _taskDataContext.AddAsync(task);
    }
    public async Task<_Task?> GetTask(long taskId)
    {
        return await _taskDataContext.Tasks.FirstOrDefaultAsync(x => x.Id == taskId);
    }
    public async Task<_Task?> GetTask(TaskTypes type, long operatingOn)
    {
        return  await _taskDataContext.Tasks.FirstOrDefaultAsync(x => x.Type == type && x.OperatingOn == operatingOn);
    }
    public async Task<List<_Task>> GetTasks(TaskStatus status)
    {
        return await _taskDataContext.Tasks.Where(x => x.Status == status).ToListAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _taskDataContext.SaveChangesAsync();
    }
    
    
}