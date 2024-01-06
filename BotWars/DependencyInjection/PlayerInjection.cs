using Communication.ServiceInterfaces;
using Communication.Services.Player;
using Microsoft.AspNetCore.Identity;
using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.Mappers;
using Shared.DataAccess.Repositories;
using Shared.DataAccess.RepositoryInterfaces;

namespace BotWars.DependencyInjection;

public static class PlayerInjection
{
    public static IServiceCollection AddPlayer(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IPlayerRepository, PlayerRepository>();
        serviceCollection.AddScoped<PlayerAdminService, PlayerAdminService>();
        serviceCollection.AddScoped<PlayerIdentifiedPlayerService, PlayerIdentifiedPlayerService>();
        serviceCollection.AddScoped<PlayerUnidentifiedPlayerService, PlayerUnidentifiedPlayerService>();
        serviceCollection.AddScoped<PlayerBannedPlayerService, PlayerBannedPlayerService>();
        serviceCollection.AddScoped<PlayerBadValidation, PlayerBadValidation>();
        serviceCollection.AddScoped<IPlayerService, PlayerUnidentifiedPlayerService>(); //modifyed only for a while due to not working purposes
        serviceCollection.AddScoped<IPlayerMapper, PlayerMapper>();
        serviceCollection.AddScoped<PlayerServiceProvider, PlayerServiceProvider>();
        serviceCollection.AddScoped<IPasswordHasher<Player>, PasswordHasher<Player>>();
        return serviceCollection;
    }
}