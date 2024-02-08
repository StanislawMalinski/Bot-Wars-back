using Coravel.Invocable;
using Coravel.Scheduling.Schedule.Interfaces;

namespace BusinessLogic.BackgroundWorkers;

public class InicjalizeWorkers : IInvocable
{
    private IScheduler _scheduler;

    public InicjalizeWorkers(IScheduler scheduler)
    {
        _scheduler = scheduler;
    }

    public async Task Invoke()
    {
        Console.WriteLine("inicjalizaca");
        _scheduler.Schedule<Synchronizer>().EverySeconds(25);
        await Task.Delay(500);
        _scheduler.Schedule<TScheduler>().EverySeconds(25);
    }
}