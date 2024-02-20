using Coravel.Invocable;
using Coravel.Scheduling.Schedule.Interfaces;

namespace Engine.BusinessLogic.BackgroundWorkers;

public class InicjalizeWorkers: IInvocable
{
    private IScheduler _scheduler;

    public InicjalizeWorkers(IScheduler scheduler)
    {
        _scheduler = scheduler;
    }

    public async Task Invoke()
    {
        Console.WriteLine("inicjalizaca");
        _scheduler.Schedule<TScheduler>().EverySeconds(25);
    }
}