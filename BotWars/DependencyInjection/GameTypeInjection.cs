using Communication.Services.GameType;
using Shared.DataAccess.Mappers;
using Shared.DataAccess.Repositories;
using Shared.DataAccess.RepositoryInterfaces;

namespace BotWars.DependencyInjection;

public static class GameTypeInjection
{
    public static IServiceCollection AddGameType(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IGameRepository, GameRepository>();
        serviceCollection.AddScoped<GameTypeAdminService, GameTypeAdminService>();
        serviceCollection.AddScoped<GameTypeIdentifiedPlayerService, GameTypeIdentifiedPlayerService>();
        serviceCollection.AddScoped<GameTypeUnidentifiedPlayerService, GameTypeUnidentifiedPlayerService>();
        serviceCollection.AddScoped<GameTypeBannedPlayerService, GameTypeBannedPlayerService>();
        serviceCollection.AddScoped<GameTypeBadValidation, GameTypeBadValidation>();
        serviceCollection.AddScoped<GameTypeService, GameTypeService>();
        serviceCollection.AddScoped<IGameTypeMapper, GameTypeMapper>();
        serviceCollection.AddScoped<GameTypeServiceProvider, GameTypeServiceProvider>();
        return serviceCollection;
    }
}