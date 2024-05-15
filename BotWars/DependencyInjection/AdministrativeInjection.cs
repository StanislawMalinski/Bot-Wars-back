using Communication.Services.Administration;
using Shared.DataAccess.Repositories;
using Shared.DataAccess.RepositoryInterfaces;

namespace BotWars.DependencyInjection;

public static class AdministrativeInjection
{
    public static IServiceCollection AddAdministrative(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IAdministrativeService, AdministrativeService>();
        return serviceCollection;
    }
}