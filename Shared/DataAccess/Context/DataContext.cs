using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.EntitiesConfigurations;
using Shared.DataAccess.Seeders;

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
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PlayerConfigurations).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TournamentConfigurations).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ArchivedMatchesConfigurations).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ArchivedMatchPlayersConfigurations).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BotConfigurations).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(GameConfigurations).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TournamentReferenceConfigurations).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PointHistoryConfigurations).Assembly);
            
            Seed(modelBuilder);
        }

        private static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>().HasData(Seeder.GeneratePlayers());
            modelBuilder.Entity<Game>().HasData(Seeder.GenerateGames());
            modelBuilder.Entity<Tournament>().HasData(Seeder.GenerateTournaments());
            modelBuilder.Entity<Bot>().HasData(Seeder.GenerateBots());
            modelBuilder.Entity<TournamentReference>().HasData(Seeder.GenerateTournamentReferences());
        }

        
    }
}
