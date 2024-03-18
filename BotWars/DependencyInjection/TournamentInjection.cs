using Communication.Services.Tournament;
using Shared.DataAccess.Mappers;
using Shared.DataAccess.Repositories;
using Shared.DataAccess.RepositoryInterfaces;

namespace BotWars.DependencyInjection;

public static class TournamentInjection
{
    public static IServiceCollection AddTournament(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<TournamentRepository, TournamentRepository>();
        serviceCollection.AddScoped<ITournamentMapper, TournamentMapper>();
        serviceCollection.AddScoped<ITournamentService, TournamentService>();
        return serviceCollection;
    }
}