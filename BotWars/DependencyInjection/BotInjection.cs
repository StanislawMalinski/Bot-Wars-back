using Communication.Services.Bot;
using Shared.DataAccess.Mappers;
using Shared.DataAccess.Repositories;
using Shared.DataAccess.RepositoryInterfaces;

namespace BotWars.DependencyInjection;

public static class BotInjection
{
    public static IServiceCollection AddBot(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IBotRepository, BotRepository>();
        serviceCollection.AddScoped<IBotService, BotService>();
        serviceCollection.AddScoped<IBotMapper, BotMapper>();
        return serviceCollection;
    }
}