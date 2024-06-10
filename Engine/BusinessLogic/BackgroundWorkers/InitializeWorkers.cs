using Coravel.Invocable;
using Coravel.Scheduling.Schedule.Interfaces;
using Engine.Services;

namespace Engine.BusinessLogic.BackgroundWorkers;

public class InitializeWorkers : IInvocable
{
    private readonly IScheduler _scheduler;
    private readonly TaskService _taskService;

    public InitializeWorkers(IScheduler scheduler, TaskService taskService)
    {
        _scheduler = scheduler;
        _taskService = taskService;
    }

    public async Task Invoke()
    {
        Console.WriteLine("Inicjalizacja");
        await _taskService.RestartTasks();
        _scheduler.Schedule<TaskClaimer>().EverySeconds(10).PreventOverlapping("Assign");
        _scheduler.Schedule<TScheduler>().EverySeconds(10).PreventOverlapping("Schedule");
        Console.WriteLine("Inicjalizacja zakończona");
    }
}