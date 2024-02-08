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
        _scheduler.Schedule<Synchronizer>().EveryMinute();
    }
}