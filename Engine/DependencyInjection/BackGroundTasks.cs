using Coravel;
using Engine.BusinessLogic.BackgroundWorkers;
using Engine.BusinessLogic.BackgroundWorkers.Resolvers;
using Engine.FileWorker;
using Shared.DataAccess.Repositories;

namespace Engine.DependencyInjection;

public static class BackGroundTasks
{
    public static IServiceCollection AddBackGroundTask(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScheduler();
        serviceCollection.AddQueue();
        serviceCollection.AddCache();
        serviceCollection.AddTransient<InitializeWorkers>();
        serviceCollection.AddTransient<TScheduler>();
        serviceCollection.AddTransient<TaskClaimer>();
        // serviceCollection.AddTransient<TournamentWorker>();
        //serviceCollection.AddTransient<GameWorker>();
        serviceCollection.AddScoped<SchedulerRepository>();
        serviceCollection.AddScoped<FileManager>();

        serviceCollection.AddScoped<TournamentResolver>();
        serviceCollection.AddScoped<MatchResolver>();
        serviceCollection.AddScoped<ValidationResolver>();
        serviceCollection.AddScoped<PointsEngineAccessor>();
        return serviceCollection;
    }
}