﻿using Communication.Services.GameType;
using Shared.DataAccess.Mappers;
using Shared.DataAccess.Repositories;
using Shared.DataAccess.RepositoryInterfaces;

namespace BotWars.DependencyInjection;

public static class GameTypeInjection
{
    public static IServiceCollection AddGameType(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IGameRepository, GameRepository>();
        serviceCollection.AddScoped<IGameTypeMapper, GameTypeMapper>();
        serviceCollection.AddScoped<IGameService, GameTypeService>();
        return serviceCollection;
    }
}