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

    public async Task<List<_Task>> TaskToDo()
    {

        DateTime data = DateTime.Now;
        return await _dataContext.Tasks.Where(x => x.ScheduledOn <= data && x.Status == TaskStatus.ToDo).ToListAsync();
        
    }

    public async Task<List<_Task>> TaskToDo(int engineId)
    {

        DateTime data = DateTime.Now;
        return await _dataContext.Tasks.Where(x => x.ScheduledOn <= data && x.Status == TaskStatus.ToDo && x.EngineId == engineId).ToListAsync();
        
    }

    public async Task<List<_Task>> UnassignedTasks()
    {

        DateTime data = DateTime.Now;
        return await _dataContext.Tasks.Where(x => x.ScheduledOn <= data && x.Status == TaskStatus.Unassigned).ToListAsync();
       
    }

    public async Task<bool> AssignTask(long taskId, int engineId)
    {
        var res = await _dataContext.Tasks.FirstOrDefaultAsync(x => x.Id == taskId);
        if (res == null || res.Status != TaskStatus.Unassigned) return false;
        res.Status = TaskStatus.ToDo;
        res.EngineId = engineId;
        await _dataContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> Taskdoing(long taskId)
    {
        var res = await _dataContext.Tasks.FirstOrDefaultAsync(x => x.Id == taskId);
        if (res == null) return false;
        res.Status = TaskStatus.Doing;
        await _dataContext.SaveChangesAsync();
        return true;
    }
}
