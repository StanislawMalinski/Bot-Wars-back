using Shared.DataAccess.Mappers;
using Shared.DataAccess.MappersInterfaces;

namespace Engine.DependencyInjection;

public static class MapperInjection
{
    public static IServiceCollection AddMappers(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IAchievementRecordMapper, AchievementRecordMapper>();
        serviceCollection.AddScoped<IAchievementTypeMapper, AchievementTypeMapper>();
        serviceCollection.AddScoped<IGameTypeMapper, GameTypeMapper > ();
        serviceCollection.AddScoped<IPlayerMapper, PlayerMapper>();
        serviceCollection.AddScoped<IPointHistoryMapper, PointHistoryMapper>();
        serviceCollection.AddScoped<ITournamentMapper, TournamentMapper>();
        serviceCollection.AddScoped<IUserSettingsMapper, UserSettingsMapper>();
        serviceCollection.AddScoped<IBotMapper, BotMapper>();
        serviceCollection.AddScoped<MatchMapper>();
        return serviceCollection;
    }
}