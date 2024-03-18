using Coravel.Invocable;
using Coravel.Scheduling.Schedule.Interfaces;
using Shared.DataAccess.Repositories;

namespace Engine.BusinessLogic.BackgroundWorkers;

public class InicjalizeWorkers: IInvocable
{
    private IScheduler _scheduler;
    private TaskRepository _taskRepository;

    public InicjalizeWorkers(IScheduler scheduler,TaskRepository taskRepository)
    {
        _scheduler = scheduler;
        _taskRepository = taskRepository;
    }

    public async Task Invoke()
    {
        Console.WriteLine("inicjalizaca");
        await _taskRepository.RestartTasks();
        _scheduler.Schedule<TScheduler>().EverySeconds(10);
        Console.WriteLine("inicjalizaca zakończona");
    }
}