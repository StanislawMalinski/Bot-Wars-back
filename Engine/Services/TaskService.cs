using System.Runtime.InteropServices.ComTypes;
using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.Enumerations;
using Shared.DataAccess.Repositories;
using Shared.Results;
using Shared.Results.ErrorResults;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;
using TaskStatus = Shared.DataAccess.Enumerations.TaskStatus;

namespace Engine.Services;

public class TaskService
{
    private readonly  TaskRepository _taskRepository;

    public TaskService(TaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }
    public async Task<HandlerResult<Success, IErrorResult>> RestartTasks()
    {
        var res = await _taskRepository.GetTasks(TaskStatus.Doing);
        foreach (var task in res)
        {
            task.Status = TaskStatus.Unassigned ;
            Console.WriteLine("co z tym zdaniem ");
        }
        await _taskRepository.SaveChangesAsync();
        Console.WriteLine("resoetownaie zadań");
        
        return new Success();
    }

    public async Task<HandlerResult<SuccessData<long>, IErrorResult>> CreateTask(TaskTypes type, long operatingOn,
        DateTime scheduledOn, TaskStatus status = TaskStatus.Unassigned)
    {
        var res = await _taskRepository.AddTask(type, operatingOn, scheduledOn, status);
        await _taskRepository.SaveChangesAsync();
        Console.WriteLine("dodano zadanie");
        return new SuccessData<long>()
        {
            Data = res.Entity.Id
        };
    }
    public async Task<HandlerResult<Success, IErrorResult>> CreateTask(_Task task)
    {
        var res = await _taskRepository.AddTask(task);
        return new Success();
    }

    public async Task<HandlerResult<SuccessData<_Task>, IErrorResult>> GetTask(long taskId)
    {
        var res = await _taskRepository.GetTask(taskId);
        if (res == null) return new EntityNotFoundErrorResult();
        return new SuccessData<_Task>()
        {
            Data = res
        };
    }


    public async Task<HandlerResult<SuccessData<TaskStatus>, IErrorResult>> CheckStatus(TaskTypes type,
        long operatingOn)
    {
        var res = await _taskRepository.GetTask(type, operatingOn);
        if (res == null) return new EntityNotFoundErrorResult();
        return new SuccessData<TaskStatus>()
        {
            Data = res.Status
        };
    }
    
    public async Task<HandlerResult<Success, IErrorResult>> TaskComplete(long taskId)
    {
        var res = await _taskRepository.GetTask(taskId);
        if (res == null) return new EntityNotFoundErrorResult();
        res.Status = TaskStatus.Done;
        await _taskRepository.SaveChangesAsync();
        return new Success();
    }
}