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
    private IScheduler _scheduler;
    private SchedulerRepository _schedulerRepository;
    private InstanceSettings _instanceSettings;
    private TaskRepository _taskRepository;

    public TScheduler(SchedulerRepository schedulerRepository, TaskRepository taskRepository, IScheduler scheduler, InstanceSettings instanceSettings)
    {
        _schedulerRepository = schedulerRepository;
        _scheduler = scheduler;
        _instanceSettings = instanceSettings;
        _taskRepository = taskRepository;
    }

    public async Task Invoke()
    {
        Console.WriteLine("Pobranie zadń do zrobienia");
        var tasks = await _schedulerRepository.TaskToDo(_instanceSettings.EngineId);
        foreach (var t in tasks)
        {

            switch (t.Type)
            {
                case TaskTypes.PlayTournament:
                    if (await _schedulerRepository.Taskdoing(t.Id))
                    {
                        _scheduler.ScheduleWithParams<TournamentWorker>(t.Id)
                            .EverySecond().Once().PreventOverlapping("TournamentWorker"+ DateTime.Now+" "+ t.Id);
                    }
                    break;
                case TaskTypes.PlayGame:
                    if (await _schedulerRepository.Taskdoing(t.Id))
                    {
                        Console.WriteLine("Shceduled rozegnie gry "+t.Id);
                        _scheduler.ScheduleWithParams<GameWorker>(t.Id).EverySecond().Once().PreventOverlapping("game worker " + t.Id); 
                    }
                    break;
                case TaskTypes.ValidateBot:
                    if (await _schedulerRepository.Taskdoing(t.Id))
                    {
                        Console.WriteLine("Shceduled bot validation "+t.Id);
                        _scheduler.ScheduleWithParams<ValidationWorker>(t.Id)
                            .EverySecond().Once().PreventOverlapping("Validation worker " + t.Id);
                        Console.WriteLine("zaskejulowyny validator");
                    }
                    break;
                case TaskTypes.ScheduleTournament:
                    var tour = await _schedulerRepository.GetTournamentNotScheduled();
                    if (tour != null)
                    {
                        tour.Status = TournamentStatus.SCHEDULED;
                        await _taskRepository.AddTask(TaskTypes.PlayTournament, tour.Id, tour.TournamentsDate,1);
                        
                    } 
                    await _schedulerRepository.TaskDone(t.Id);
                    await _schedulerRepository.SaveChangeAsync();
                    break;
                case TaskTypes.ScheduleValidation:
                    var bot = await _schedulerRepository.GetBotNotValidated();
                    if (bot != null)
                    {
                        bot.Validation = BotStatus.NotValidated;
                        await _taskRepository.AddTask(TaskTypes.ValidateBot, bot.Id, DateTime.Now,1);
                        
                    } 
                    await _schedulerRepository.TaskDone(t.Id);
                    await _schedulerRepository.SaveChangeAsync();
                    break;
            }
        }
        Console.WriteLine("zadania wykonane");
    }
}