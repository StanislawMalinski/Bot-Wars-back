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
        serviceCollection.AddScoped<IPlayerMapper, PlayerMapper>();
        serviceCollection.AddScoped<IPlayerService, PlayerService>();
        serviceCollection.AddScoped<IPasswordHasher<Player>, PasswordHasher<Player>>();
        return serviceCollection;
    }
}