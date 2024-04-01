using Coravel.Invocable;
using Coravel.Queuing.Interfaces;
using Coravel.Scheduling.Schedule.Interfaces;
using Shared.DataAccess.Context;
using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.Enumerations;
using Shared.DataAccess.Repositories;

namespace Engine.BusinessLogic.BackgroundWorkers;

public class TScheduler: IInvocable
{
    private DataContext _taskDataContext;
    private IQueue _queue;
    private IScheduler _scheduler;
    private SchedulerRepository _schedulerRepository;

    public TScheduler(DataContext taskDataContext, IQueue queue, SchedulerRepository schedulerRepository, IScheduler scheduler)
    {
        _taskDataContext = taskDataContext;
        _queue = queue;
        _schedulerRepository = schedulerRepository;
        _scheduler = scheduler;
    }

    public async Task Invoke()
    {
        Console.WriteLine("Pobranie zadń do zrobienia");
        var tasks = (await _schedulerRepository.TaskToDo()).Match(x=>x.Data,x=>new List<_Task>());
        foreach (var t in tasks)
        {

            switch (t.Type)
            {
                case TaskTypes.PlayTournament:
                    if ((await _schedulerRepository.Taskdoing(t.Id)).IsSuccess)
                    {
                        _scheduler.ScheduleWithParams<TournamentWorker>(t.Id)
                            .EverySecond().Once().PreventOverlapping("TournamentWorker"+ DateTime.Now+" "+ t.Id);
                    }
                    break;
                case TaskTypes.PlayGame:
                    if ((await _schedulerRepository.Taskdoing(t.Id)).IsSuccess)
                    {
                        Console.WriteLine("Shceduled "+t.Id);
                        _scheduler.ScheduleWithParams<GameWorker>(t.Id).EverySecond().Once().PreventOverlapping("game worker " + t.Id);    
                    }
                    break;
                case TaskTypes.ValidateBot:
                    if ((await _schedulerRepository.Taskdoing(t.Id)).IsSuccess)
                    {
                        _scheduler.ScheduleWithParams<ValidationWorker>(t.Id).EverySecond().Once().PreventOverlapping("Validation worker " + t.Id);
                    }
                    break;
            }
        }
        Console.WriteLine("zadania wykonane");
    }
}