using Communication.Services.Administration;
using Shared.DataAccess.Repositories;
using Shared.DataAccess.RepositoryInterfaces;

namespace BotWars.DependencyInjection;

public static class AdministrativeInjection
{
    public static IServiceCollection AddAdministrative(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IAdministrativeRepository, AdministrativeRepository>();
        serviceCollection.AddScoped<AdministrativeServiceProvider, AdministrativeServiceProvider>();
        serviceCollection.AddScoped<AdministrativeAdminService, AdministrativeAdminService>();
        serviceCollection.AddScoped<AdministrativeIdentifiedPlayerService, AdministrativeIdentifiedPlayerService>();
        serviceCollection.AddScoped<AdministrativeUnidentifiedPlayerService, AdministrativeUnidentifiedPlayerService>();
        serviceCollection.AddScoped<AdministrativeBannedPlayerService, AdministrativeBannedPlayerService>();
        serviceCollection.AddScoped<AdministrativeBadValidationService, AdministrativeBadValidationService>();
        serviceCollection.AddScoped<AdministrativeService, AdministrativeService>();
        return serviceCollection;
    }
}