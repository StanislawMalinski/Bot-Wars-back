using Shared.DataAccess.Mappers;
using Shared.DataAccess.Repositories;

namespace BotWars.DependencyInjection;

public static class UserSettingsInjection
{
    public static IServiceCollection AddUserSettings(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<UserSettingsRepository, UserSettingsRepository>();
        serviceCollection.AddScoped<IUserSettingsMapper, UserSettingsMapper>();
        return serviceCollection;
    }
}