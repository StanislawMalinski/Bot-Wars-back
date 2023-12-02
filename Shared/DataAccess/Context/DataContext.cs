using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.EntitiesConfigurations;

namespace Shared.DataAccess.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Game> Games { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Bot> Bots { get; set; }
        public DbSet<Tournament> Tournaments { get; set; }
        public DbSet<ArchivedMatches> ArchivedMatches { get; set; }
        public DbSet<ArchivedMatchPlayers> ArchivedMatchPlayers { get; set; }
        public DbSet<TournamentReference> TournamentReferences { get; set; }
        public DbSet<PointHistory> PointHistories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*
            ConfigureGame(modelBuilder.Entity<Game>());
            ConfigurePlayer(modelBuilder.Entity<Player>());
            ConfigureBot(modelBuilder.Entity<Bot>());
            ConfigureTournament(modelBuilder.Entity<Tournament>());
            ConfigureArchivedMatches(modelBuilder.Entity<ArchivedMatches>());
            ConfigureArchivedMatchPlayers(modelBuilder.Entity<ArchivedMatchPlayers>());
            ConfigureTournamentReference(modelBuilder.Entity<TournamentReference>());*/
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PlayerConfigurations).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TournamentConfigurations).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ArchivedMatchesConfigurations).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ArchivedMatchPlayersConfigurations).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BotConfigurations).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(GameConfigurations).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TournamentReferenceConfigurations).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PointHistoryConfigurations).Assembly);
            
        }

        
    }
}
