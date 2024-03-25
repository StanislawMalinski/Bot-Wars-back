using Communication.ServiceInterfaces;
using Shared.DataAccess.Mappers;
using Shared.DataAccess.Repositories;
using Shared.DataAccess.RepositoryInterfaces;

namespace BotWars.DependencyInjection;

public static class PointsInjection
{
    public static IServiceCollection AddPointsSettings(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IPointsService, PointsService>();
        serviceCollection.AddScoped<IPointsRepository, PointRepository>();
        serviceCollection.AddScoped<IPointHistoryMapper, PointHistoryMapper>();
        return serviceCollection;
    }
}