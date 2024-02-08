using BusinessLogic.BackgroundWorkers;
using Coravel;
using Shared.DataAccess.Repositories;

namespace BotWars.DependencyInjection;

public static class BackGroundTasks
{
    public static IServiceCollection AddBackGroundTask(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScheduler();
        serviceCollection.AddQueue();
        serviceCollection.AddCache();
        serviceCollection.AddTransient<InicjalizeWorkers>();
        serviceCollection.AddTransient<TScheduler>();
        serviceCollection.AddTransient<Synchronizer>();
        serviceCollection.AddTransient<TournamentWorker>();
        serviceCollection.AddTransient<GameWorker>();
        serviceCollection.AddScoped<SynchronizedRepository>();
        return serviceCollection;
    }
}