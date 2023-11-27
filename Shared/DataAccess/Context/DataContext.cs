using Microsoft.EntityFrameworkCore;
using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.EntitiesConfigurations;

namespace Shared.DataAccess.Context
{
    public class DataContext : DbContext
    {
        public DbSet<Player> Players { get; set; } = null!;
        public DbSet<Tournament> Tournaments { get; set; } = null!;
        public DbSet<ArchivedMatches> ArchivedMatchesEnumerable { get; set; } = null!;
        public DbSet<ArchivedMatchPlayers> ArchivedMatchPlayersEnumerable { get; set; } = null!;
        public DbSet<Bot> Bots { get; set; } = null!;
        public DbSet<Game> Games { get; set; } = null!;
        public DbSet<GameType> GameTypes { get; set; } = null!;
        public DbSet<TournamentReference> TournamentReferences { get; set; } = null!;
        
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PlayerConfigurations).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TournamentConfigurations).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ArchivedMatchesConfigurations).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ArchivedMatchPlayersConfigurations).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BotConfigurations).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(GameConfigurations).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(GameTypeConfigurations).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TournamentReferenceConfigurations).Assembly);
        }
    }
}
