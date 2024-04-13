using Coravel.Invocable;
using Coravel.Queuing.Interfaces;
using Coravel.Scheduling.Schedule.Interfaces;
using Shared.DataAccess.Context;
using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.Repositories;

namespace Engine.BusinessLogic.BackgroundWorkers
{
    public class TaskClaimer : IInvocable
    {
        private SchedulerRepository _schedulerRepository;
        private InstanceSettings _instanceSettings;

        public TaskClaimer(SchedulerRepository schedulerRepository, InstanceSettings instanceSettings) 
        {
            _schedulerRepository = schedulerRepository;
            _instanceSettings = instanceSettings;
        }

        public async Task Invoke()
        {
            var tasks = (await _schedulerRepository.UnassignedTasks()).Match(x => x.Data, x => new List<_Task>());
            foreach (var t in tasks)
            {
                await _schedulerRepository.AssignTask(t.Id, _instanceSettings.EngineId);
            }
            Console.WriteLine("Tasks Assigned");
        }
    }
}
