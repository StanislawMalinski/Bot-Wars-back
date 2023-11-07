using BotWars.Gry;
using BotWars.RockPaperScissorsData;
using BotWars.Services;
using Microsoft.EntityFrameworkCore;

namespace BotWars.Models
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
        public DbSet<RockPaperScissors> RockPaperScissors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // fluent api 
            

            modelBuilder.Entity<Game>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Game>()
                .Property(p => p.NumbersOfPlayer)
                .IsRequired();

            modelBuilder.Entity<Game>()
                .Property(p => p.LastModification)
                .IsRequired();

            modelBuilder.Entity<Game>()
                .Property(p => p.GameFile)
                .IsRequired();

            modelBuilder.Entity<Game>()
                .Property(p => p.GameInstructions)
                .IsRequired();

            modelBuilder.Entity<Game>()
                .Property(p => p.InterfaceDefinition)
                .IsRequired();

            modelBuilder.Entity<Game>()
                .Property(p => p.IsAvaiableForPlay)
                .IsRequired();
            //Player
            modelBuilder.Entity<Player>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Player>()
                .HasIndex(x => x.email).IsUnique();

            modelBuilder.Entity<Player>()
                .Property(p => p.login)
                .IsRequired();
            //Bot
            modelBuilder.Entity<Bot>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Player>()
                .HasOne(x => x.Bot)
                .WithOne(b => b.Players)
                .HasPrincipalKey<Bot>(b => b.PlayerId);

            modelBuilder.Entity<Bot>()
               .Property(p => p.PlayerId)
               .IsRequired();

            modelBuilder.Entity<Game>()
                .HasOne(x => x.Bot)
                .WithOne(b => b.Games)
                .HasPrincipalKey<Bot>(b => b.GameId);

            modelBuilder.Entity<Bot>()
               .Property(p => p.GameId)
               .IsRequired();

            modelBuilder.Entity<Bot>()
               .Property(p => p.BotFile)
               .IsRequired();

            //Tournament
            modelBuilder.Entity<Tournament>()
               .HasKey(x => x.Id);

            modelBuilder.Entity<Tournament>()
                .HasOne(x => x.Game)
                .WithMany(b => b.Tournaments)
                .HasForeignKey(x => x.GameId);

            modelBuilder.Entity<Tournament>()
               .Property(p => p.GameId)
               .IsRequired();

            modelBuilder.Entity<Tournament>()
               .Property(p => p.PlayersLimi)
               .IsRequired();

            modelBuilder.Entity<Tournament>()
               .Property(p => p.PostedDate)
               .IsRequired();

            modelBuilder.Entity<Tournament>()
               .Property(p => p.TournamentsDate)
               .IsRequired();

            modelBuilder.Entity<Tournament>()
               .Property(p => p.WasPlayedOut)
               .IsRequired();

            modelBuilder.Entity<Tournament>()
               .Property(p => p.Contrains)
               .IsRequired();
            //ArchivedMatches
            modelBuilder.Entity<ArchivedMatches>()
               .HasKey(x => x.Id);

            modelBuilder.Entity<ArchivedMatches>()
                .HasOne(a => a.Game)
                .WithMany(b => b.ArchivedMatches)
                .HasForeignKey(a => a.GameId);

            modelBuilder.Entity<ArchivedMatches>()
               .Property(p => p.GameId)
               .IsRequired();

            modelBuilder.Entity<Tournament>()
                .HasOne(a => a.ArchivedMatches)
                .WithOne(b => b.Tournament)
                .HasForeignKey<ArchivedMatches>(b => b.TournamentsId);

            modelBuilder.Entity<ArchivedMatches>()
               .Property(p => p.TournamentsId)
               .IsRequired();

            modelBuilder.Entity<ArchivedMatches>()
               .Property(p => p.Played)
               .IsRequired();

            modelBuilder.Entity<ArchivedMatches>()
               .Property(p => p.Match)
               .IsRequired();

            //ArchivedMatchPlayer

            modelBuilder.Entity<ArchivedMatchPlayers>()
               .HasKey(x =>x.Id);


            modelBuilder.Entity<Player>()
                 .HasOne(a => a.ArchivedMatchPlayers)
                 .WithOne(b => b.Player)
                 .HasPrincipalKey<ArchivedMatchPlayers>(b => b.PlayerId);

            modelBuilder.Entity<ArchivedMatchPlayers>()
               .Property(p => p.PlayerId)
               .IsRequired();

            //modelBuilder.Entity<ArchivedMatchPlayers>()
              //.Property(p => p.Matchld)
              //.IsRequired();

            modelBuilder.Entity<ArchivedMatchPlayers>()
               .Property(p => p.TournamentId)
               .IsRequired();

            modelBuilder.Entity<Tournament>()
                .HasOne(a => a.ArchivedMatchPlayers)
                .WithOne(b => b.Tournament)
                .HasForeignKey<ArchivedMatchPlayers>(b => b.TournamentId);
            // rock paper sciros do wyrzucnei a w tym foramcjie najpweniej;
           

            modelBuilder.Entity<RockPaperScissors>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<RockPaperScissors>()
                .Property(p => p.PlayerOneName)
                .IsRequired();

            modelBuilder.Entity<RockPaperScissors>()
                .Property(p => p.PlayerTwoName)
                .IsRequired();

            modelBuilder.Entity<RockPaperScissors>()
                .Property(p => p.SymbolPlayerOne);

            modelBuilder.Entity<RockPaperScissors>()
                .Property(p => p.SymbolPlayerTwo);

            modelBuilder.Entity<RockPaperScissors>()
                .Property(p => p.Winner);
            // data seed 


        }
    }
}
