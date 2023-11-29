using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.DataAccess.DataBaseEntities;

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureGame(modelBuilder.Entity<Game>());
            ConfigurePlayer(modelBuilder.Entity<Player>());
            ConfigureBot(modelBuilder.Entity<Bot>());
            ConfigureTournament(modelBuilder.Entity<Tournament>());
            ConfigureArchivedMatches(modelBuilder.Entity<ArchivedMatches>());
            ConfigureArchivedMatchPlayers(modelBuilder.Entity<ArchivedMatchPlayers>());
            ConfigureTournamentReference(modelBuilder.Entity<TournamentReference>());
        }

        private void ConfigureGame(EntityTypeBuilder<Game> entity)
        {
            entity.HasKey(x => x.Id);
            entity.Property(p => p.NumbersOfPlayer).IsRequired();
            entity.Property(p => p.LastModification).IsRequired();
            entity.Property(p => p.GameFile).IsRequired();
            entity.Property(p => p.GameInstructions).IsRequired();
            entity.Property(p => p.InterfaceDefinition).IsRequired();
            entity.Property(p => p.IsAvaiableForPlay).IsRequired();
        }

        private void ConfigurePlayer(EntityTypeBuilder<Player> entity)
        {
            entity.HasKey(x => x.Id);
            entity.Property(p => p.email).HasColumnType("VARCHAR").HasMaxLength(30).IsRequired();
            entity.Property(p => p.login).HasColumnType("VARCHAR").HasMaxLength(20).IsRequired();
        }

        private void ConfigureBot(EntityTypeBuilder<Bot> entity)
        {
            entity.HasKey(x => x.Id);
            entity.Property(p => p.BotFile).IsRequired();
        }

        private void ConfigureTournament(EntityTypeBuilder<Tournament> entity)
        {
            entity.HasKey(x => x.Id);
            entity.Property(p => p.TournamentTitles).HasColumnType("VARCHAR").HasMaxLength(30).IsRequired();
            entity.Property(p => p.Description).HasColumnType("VARCHAR").HasMaxLength(200).IsRequired();
            entity.HasOne(x => x.Game).WithMany(b => b.Tournaments).HasForeignKey(x => x.GameId);
            entity.Property(p => p.GameId).IsRequired();
            entity.Property(p => p.PlayersLimit).IsRequired();
            entity.Property(p => p.PostedDate).IsRequired();
            entity.Property(p => p.TournamentsDate).IsRequired();
            entity.Property(p => p.WasPlayedOut).IsRequired();
            entity.Property(p => p.Contrains).IsRequired();
        }

        private void ConfigureArchivedMatches(EntityTypeBuilder<ArchivedMatches> entity)
        {
            entity.HasKey(x => x.Id);
            entity.HasOne(a => a.Game).WithMany(b => b.ArchivedMatches).HasForeignKey(a => a.GameId).IsRequired().OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(a => a.Tournament).WithMany(b => b.ArchivedMatches).HasForeignKey(a => a.TournamentsId).IsRequired().OnDelete(DeleteBehavior.NoAction);
            entity.Property(p => p.Played).IsRequired();
            entity.Property(p => p.Match).IsRequired();
        }

        private void ConfigureArchivedMatchPlayers(EntityTypeBuilder<ArchivedMatchPlayers> entity)
        {
            entity.HasKey(x => x.Id);
            entity.HasOne(a => a.Player).WithMany(b => b.ArchivedMatchPlayers).HasForeignKey(a => a.PlayerId).IsRequired();
            entity.HasOne(a => a.archivedMatches).WithMany(b => b.ArchivedMatchPlayers).HasForeignKey(a => a.MatchId).IsRequired();
            entity.Property(p => p.TournamentId).IsRequired();
        }

        private void ConfigureTournamentReference(EntityTypeBuilder<TournamentReference> entity)
        {
            entity.HasKey(x => x.Id);
            entity.Property(p => p.bodId).IsRequired();
        }
    }
}
