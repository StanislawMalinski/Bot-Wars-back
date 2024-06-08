using Communication.ServiceInterfaces;
using Communication.Services.Achievement;
using Shared.DataAccess.Mappers;
using Shared.DataAccess.Repositories;
using Shared.DataAccess.RepositoryInterfaces;

namespace BotWars.DependencyInjection;

public static class AchievementInjection
{
    public static IServiceCollection AddAchievements(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IAchievementsRepository, AchievementsRepository>();
        serviceCollection.AddScoped<IAchievementRecordMapper, AchievementRecordMapper>();
        serviceCollection.AddScoped<IAchievementTypeMapper, AchievementTypeMapper>();
        serviceCollection.AddScoped<IAchievementService, AchievementService>();

        return serviceCollection;
    }
}