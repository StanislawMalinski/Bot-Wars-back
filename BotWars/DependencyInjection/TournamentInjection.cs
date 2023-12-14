using Communication.Services.Tournament;
using Shared.DataAccess.Mappers;
using Shared.DataAccess.Repositories;

namespace BotWars.DependencyInjection;

public static class TournamentInjection
{
    public static IServiceCollection AddTournament(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<TournamentAdminService, TournamentAdminService>();
        serviceCollection.AddScoped<TournamentIdentifiedPlayerService, TournamentIdentifiedPlayerService>();
        serviceCollection.AddScoped<TournamentUnidentifiedPlayerService, TournamentUnidentifiedPlayerService>();
        serviceCollection.AddScoped<TournamentBannedPlayerService, TournamentBannedPlayerService>();
        serviceCollection.AddScoped<TournamentBadValidation, TournamentBadValidation>();
        serviceCollection.AddScoped<TournamentService, TournamentService>();
        serviceCollection.AddScoped<TournamentRepository, TournamentRepository>();
        serviceCollection.AddScoped<ITournamentMapper, TournamentMapper>();
        serviceCollection.AddScoped<TournamentServiceProvider, TournamentServiceProvider>();
        return serviceCollection;
    }
}