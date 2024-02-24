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
    private TaskDataContext _taskDataContext;

    public SchedulerRepository(DataContext dataContext, TaskDataContext taskDataContext)
    {
        _dataContext = dataContext;
        _taskDataContext = taskDataContext;
    }

    public async Task<HandlerResult<SuccessData<List<_Task>>, IErrorResult>> TaskToDo()
    {

        DateTime data = DateTime.Now;
        var result = await _taskDataContext.Tasks.Where(x => x.ScheduledOn >= data && x.Status == TaskStatus.ToDo).ToListAsync();
        //var result = await _taskDataContext.Tasks.Where(x => x.ScheduledOn < data && x.Status == false).ToListAsync();
        return new SuccessData<List<_Task>>()
        {
            Data = result
        };
    }

    public async Task<HandlerResult<Success, IErrorResult>> Taskdoing(long taskId)
    {
        var res = await _taskDataContext.Tasks.FirstOrDefaultAsync(x => x.Id == taskId);
        if (res == null)
        {
            return new EntityNotFoundErrorResult();
        }
        res.Status = TaskStatus.Doing;
        await _taskDataContext.SaveChangesAsync();
        return new Success();
    }
}