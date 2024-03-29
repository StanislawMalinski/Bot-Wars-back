﻿using Engine.BusinessLogic.BackgroundWorkers.Resolvers;
using Shared.DataAccess.Repositories;
using Shared.DataAccess.RepositoryInterfaces;

namespace Engine.DependencyInjection;

public static class RepositoryInjection
{
    public static IServiceCollection AddRepositories(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IAchievementsRepository, AchievementsRepository>();
        serviceCollection.AddScoped<IAdministrativeRepository, AdministrativeRepository>();
        serviceCollection.AddScoped<IGameRepository, GameRepository>();
        //serviceCollection.AddScoped<IPlayerRepository, PlayerRepository>();
        serviceCollection.AddScoped<IPointHistoryRepository, PointHistoryRepository>();
        serviceCollection.AddScoped<IPointsRepository, PointRepository>();
        serviceCollection.AddScoped<TournamentRepository>();
        serviceCollection.AddScoped<IUserSettingsRepository, UserSettingsRepository>();
        serviceCollection.AddScoped<BotRepository>();
        serviceCollection.AddScoped<MatchRepository>();
        serviceCollection.AddScoped<TaskRepository>();
        serviceCollection.AddScoped<AchievementHandlerService>();
        return serviceCollection;
    }
}