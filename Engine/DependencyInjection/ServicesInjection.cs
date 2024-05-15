using Engine.Services;

namespace Engine.DependencyInjection;

public static class ServicesInjection
{
    public static IServiceCollection AddServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<GameService>();
        serviceCollection.AddScoped<AchievementService>();
        serviceCollection.AddScoped<BotService>();
        serviceCollection.AddScoped<MatchService>();
        serviceCollection.AddScoped<TournamentService>();
        serviceCollection.AddScoped<TaskService>();
        return serviceCollection;
    }
}