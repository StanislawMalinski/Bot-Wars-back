using Coravel;
using Engine.BusinessLogic.BackgroundWorkers;
using Shared.DataAccess.Repositories;

namespace Engine.DependencyInjection;

public static class BackGroundTasks
{
    public static IServiceCollection AddBackGroundTask(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScheduler();
        serviceCollection.AddQueue();
        serviceCollection.AddCache();
        serviceCollection.AddTransient<InicjalizeWorkers>();
        serviceCollection.AddTransient<TScheduler>();
        // serviceCollection.AddTransient<TournamentWorker>();
        serviceCollection.AddTransient<GameWorker>();
        serviceCollection.AddScoped<SchedulerRepository>();
        return serviceCollection;
    }
}