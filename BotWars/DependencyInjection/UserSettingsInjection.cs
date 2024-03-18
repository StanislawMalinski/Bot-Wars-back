using Communication.ServiceInterfaces;
using Communication.Services.UserSettings;
using Shared.DataAccess.Mappers;
using Shared.DataAccess.Repositories;
using Shared.DataAccess.RepositoryInterfaces;

namespace BotWars.DependencyInjection;

public static class UserSettingsInjection
{
    public static IServiceCollection AddUserSettings(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IUserSettingsRepository, UserSettingsRepository>();
        serviceCollection.AddScoped<IUserSettingsMapper, UserSettingsMapper>();
        serviceCollection.AddScoped<IUserSettingsService, UserSettingsService>();
        return serviceCollection;
    }
}