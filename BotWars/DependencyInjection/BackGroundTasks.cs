using BusinessLogic.BackgroundWorkers;
using Coravel;

namespace BotWars.DependencyInjection;

public static class BackGroundTasks
{
    public static IServiceCollection AddBackGroundTask(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScheduler();
        serviceCollection.AddTransient<TScheduler>();
        serviceCollection.AddTransient<Synchronizer>();
        return serviceCollection;
    }
}