//using Coravel.Invocable;
//using Coravel.Scheduling.Schedule.Interfaces;
//using FileGatherer;
//namespace Engine.BusinessLogic.BackgroundWorkers;


//public class SeederInvocable : IInvocable
//{
//    private IScheduler _scheduler;

//    public SeederInvocable(IScheduler scheduler)
//    {
//        _scheduler = scheduler;
//    }

//    public async Task Invoke()
//    {
//        Console.WriteLine("Seeding");
//        await _taskRepository.RestartTasks();
//        _scheduler.Schedule<Seeder>().EverySeconds(10).PreventOverlapping("Assign");
//        Console.WriteLine("Seeding Complete");
//    }
//}