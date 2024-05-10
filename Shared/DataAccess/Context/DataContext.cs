using Microsoft.EntityFrameworkCore;
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
        public DbSet<Bot?> Bots { get; set; }
        public DbSet<Tournament> Tournaments { get; set; }
        public DbSet<Matches> Matches { get; set; }
        public DbSet<MatchPlayers> MatchPlayers { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<TournamentReference> TournamentReferences { get; set; }
        public DbSet<PointHistory> PointHistories { get; set; }
        public DbSet<UserSettings> UserSettings { get; set; }
        public DbSet<AchievementRecord> AchievementRecord { get; set; }
        public DbSet<AchievementType> AchievementType { get; set; }
        public DbSet<AchievementThresholds> AchievementThresholds { get; set; }
        public DbSet<_Task> Tasks { get; set; }
        
        public DbSet<NotificationOutbox> NotificationOutboxes { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(RoleConfigurations).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PlayerConfigurations).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserSettingsConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TournamentConfigurations).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MatchesConfigurations).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MatchPlayersConfigurations).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BotConfigurations).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(GameConfigurations).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TournamentReferenceConfigurations).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PointHistoryConfigurations).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AchievementRecordConfigurations).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AchievementTypeConfigurations).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AchievementThresholdConfigurations).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(NotificationOutboxConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TaskConfiguration).Assembly);
            Seed(modelBuilder);
        }

        private static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(Seeder.GenerateRoles());
            modelBuilder.Entity<Player>().HasData(Seeder.GeneratePlayers());
            modelBuilder.Entity<Game>().HasData(Seeder.GenerateGames());
            modelBuilder.Entity<Tournament>().HasData(Seeder.GenerateTournaments());
            modelBuilder.Entity<Bot>().HasData(Seeder.GenerateBots());
            modelBuilder.Entity<TournamentReference>().HasData(Seeder.GenerateTournamentReferences());
            modelBuilder.Entity<AchievementType>().HasData(Seeder.GenerateAchievementTypes());
            modelBuilder.Entity<AchievementThresholds>().HasData(Seeder.GenerateAchievementThresholds());
            modelBuilder.Entity<AchievementRecord>().HasData(Seeder.GenerateAchievementRecords());
        }
    }
}