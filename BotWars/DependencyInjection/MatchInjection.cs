using Communication.ServiceInterfaces;
using Communication.Services.Matches;
using Shared.DataAccess.Mappers;
using Shared.DataAccess.Repositories;

namespace BotWars.DependencyInjection;

public static class MatchInjection
{
    public static IServiceCollection AddMatch(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IMatchService, MatchService>();
        serviceCollection.AddScoped<MatchRepository, MatchRepository>();
        serviceCollection.AddScoped<MatchMapper, MatchMapper>();
        return serviceCollection;
    }
}