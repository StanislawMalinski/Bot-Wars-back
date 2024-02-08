using Coravel.Events.Interfaces;
using Coravel.Invocable;
using Coravel.Queuing.Broadcast;
using Coravel.Queuing.Interfaces;
using Coravel.Scheduling.Schedule;
using Coravel.Scheduling.Schedule.Interfaces;
using Shared.DataAccess.Context;

namespace BusinessLogic.BackgroundWorkers;

public class TScheduler : IInvocable
{
    private TaskDataContext _taskDataContext;
    private IQueue _queue;
    public TScheduler(TaskDataContext taskDataContext, IQueue queue, IScheduler scheduler)
    {
        _taskDataContext = taskDataContext;
        _queue = queue;
    }

    public async Task Invoke()
    {
        Console.WriteLine("hsa");
        int a = 51;
        Console.WriteLine("hsa"+ a);
        //var assa = _queue.QueueInvocableWithPayload<TournamentWorker, int>(2);


        //_scheduler.Schedule<TournamentWorker>().EverySeconds(5).Once();

    }
}