using Coravel.Invocable;
using Shared.DataAccess.Repositories;

namespace Engine.BusinessLogic.BackgroundWorkers;

public class TaskClaimer : IInvocable
{
    private readonly InstanceSettings _instanceSettings;
    private readonly SchedulerRepository _schedulerRepository;

    public TaskClaimer(SchedulerRepository schedulerRepository, InstanceSettings instanceSettings)
    {
        _schedulerRepository = schedulerRepository;
        _instanceSettings = instanceSettings;
    }

    public async Task Invoke()
    {
        var tasks = await _schedulerRepository.UnassignedTasks();
        foreach (var t in tasks) await _schedulerRepository.AssignTask(t.Id, _instanceSettings.EngineId);
        Console.WriteLine("Tasks Assigned");
    }
}