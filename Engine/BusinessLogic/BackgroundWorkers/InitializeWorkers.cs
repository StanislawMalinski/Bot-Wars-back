using Coravel.Invocable;
using Coravel.Scheduling.Schedule.Interfaces;
using Shared.DataAccess.Repositories;

namespace Engine.BusinessLogic.BackgroundWorkers;

public class InitializeWorkers: IInvocable
{
    private IScheduler _scheduler;
    private TaskRepository _taskRepository;

    public InitializeWorkers(IScheduler scheduler,TaskRepository taskRepository)
    {
        _scheduler = scheduler;
        _taskRepository = taskRepository;
    }

    public async Task Invoke()
    {
        Console.WriteLine("Inicjalizacja");
        await _taskRepository.RestartTasks();
        _scheduler.Schedule<TaskClaimer>().EverySeconds(10).PreventOverlapping("Assign");
        _scheduler.Schedule<TScheduler>().EverySeconds(10).PreventOverlapping("Schedule");
        Console.WriteLine("Inicjalizacja zakończona");
    }
}