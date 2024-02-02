using Shared.DataAccess.Mappers;
using Shared.DataAccess.Repositories;
using Shared.DataAccess.RepositoryInterfaces;

namespace BotWars.DependencyInjection;

public static class PointsInjection
{
    public static IServiceCollection AddPointsSettings(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<PointsAdminService, PointsAdminService>();
        serviceCollection.AddScoped<PointsBadValidationService, PointsBadValidationService>();
        serviceCollection.AddScoped<PointsBannedPlayerService, PointsBannedPlayerService>();
        serviceCollection.AddScoped<PointsIdentifiedPlayerService, PointsIdentifiedPlayerService>();
        serviceCollection.AddScoped<PointsUnidentifiedPlayerService, PointsUnidentifiedPlayerService>();
        serviceCollection.AddScoped<PointsService, PointsService>();
        serviceCollection.AddScoped<PointsServiceProvider, PointsServiceProvider>();
        serviceCollection.AddScoped<IPointsRepository, PointRepository>();
        serviceCollection.AddScoped<IPointHistoryMapper, PointHistoryMapper>();
        return serviceCollection;
    }
}