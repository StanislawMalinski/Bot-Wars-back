using Microsoft.EntityFrameworkCore;
using Shared.DataAccess.Context;
using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.Enumerations;
using TaskStatus = Shared.DataAccess.Enumerations.TaskStatus;

namespace Shared.DataAccess.Repositories;

public class SchedulerRepository
{
    private readonly DataContext _dataContext;

    public SchedulerRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<List<_Task>> TaskToDo()
    {
        var data = DateTime.Now;
        return await _dataContext.Tasks.Where(x => x.ScheduledOn <= data && x.Status == TaskStatus.ToDo).ToListAsync();
    }

    public async Task<List<_Task>> TaskToDo(int engineId)
    {
        var data = DateTime.Now;
        return await _dataContext.Tasks
            .Where(x => x.ScheduledOn <= data && x.Status == TaskStatus.ToDo && x.EngineId == engineId).ToListAsync();
    }

    public async Task<List<_Task>> UnassignedTasks()
    {
        var data = DateTime.Now;
        return await _dataContext.Tasks.Where(x => x.ScheduledOn <= data && x.Status == TaskStatus.Unassigned)
            .ToListAsync();
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

    public async Task<Tournament?> GetTournamentNotScheduled()
    {
        return await _dataContext.Tournaments.FirstOrDefaultAsync(x => x.Status == TournamentStatus.NOTSCHEDULED);
    }

    public async Task<Bot?> GetBotNotValidated()
    {
        return await _dataContext.Bots.FirstOrDefaultAsync(x => x.Validation == BotStatus.ToScheduleForValidation);
    }

    public async Task TaskDone(long taskId)
    {
        var res = await _dataContext.Tasks.FindAsync(taskId);
        if (res != null) res.Status = TaskStatus.Done;
    }

    public async Task SaveChangeAsync()
    {
        await _dataContext.SaveChangesAsync();
    }
}