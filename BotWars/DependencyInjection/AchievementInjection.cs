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
        serviceCollection.AddScoped<AchievementService, AchievementService>();
        serviceCollection.AddScoped<AchievementServiceProvider, AchievementServiceProvider>();
        serviceCollection.AddScoped<AchievementIdentifiedPlayerService, AchievementIdentifiedPlayerService>();
        serviceCollection.AddScoped<AchievementUnidentifiedPlayerService, AchievementUnidentifiedPlayerService>();
        serviceCollection.AddScoped<AchievementAdminService, AchievementAdminService>();
        serviceCollection.AddScoped<AchievementBadValidationService, AchievementBadValidationService>();
        serviceCollection.AddScoped<AchievementBannedPlayerService, AchievementBannedPlayerService>();
        return serviceCollection;
    }
}