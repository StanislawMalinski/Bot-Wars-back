using BotWars.Gry;
using BotWars.Services;
using BotWars.TournamentData;
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
                .Property(p => p.email)
                .HasColumnType("VARCHAR").HasMaxLength(30)
                .IsRequired();

            modelBuilder.Entity<Player>()
                .Property(p => p.login)
                .HasColumnType("VARCHAR").HasMaxLength(20)
                .IsRequired();
            //Bot
            modelBuilder.Entity<Bot>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Player>()
                .HasMany(x => x.Bot)
                .WithOne(b => b.Players)
                .HasForeignKey(b => b.PlayerId)
                .IsRequired();

          

            modelBuilder.Entity<Game>()
                .HasMany(x => x.Bot)
                .WithOne(b => b.Games)
                .HasForeignKey(b => b.GameId)
                .IsRequired();

           

            modelBuilder.Entity<Bot>()
               .Property(p => p.BotFile)
               .IsRequired();

            //Tournament
            modelBuilder.Entity<Tournament>()
               .HasKey(x => x.Id);

            modelBuilder.Entity<Tournament>()
                .Property(p => p.TournamentTitles)
                .HasColumnType("VARCHAR").HasMaxLength(30)
                .IsRequired();
            modelBuilder.Entity<Tournament>()
                .Property(p => p.Description)
                .HasColumnType("VARCHAR").HasMaxLength(200)
                .IsRequired();

            modelBuilder.Entity<Tournament>()
                .HasOne(x => x.Game)
                .WithMany(b => b.Tournaments)
                .HasForeignKey(x => x.GameId);

            modelBuilder.Entity<Tournament>()
               .Property(p => p.GameId)
               .IsRequired();

            modelBuilder.Entity<Tournament>()
               .Property(p => p.PlayersLimit)
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
                .HasForeignKey(a => a.GameId)
                .IsRequired();

            modelBuilder.Entity<ArchivedMatches>()
                .HasOne(a => a.Tournament)
                .WithMany(b => b.ArchivedMatches)
                .HasForeignKey(a => a.TournamentsId)
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


            modelBuilder.Entity<ArchivedMatchPlayers>()
                .HasOne(a => a.Player)
                .WithMany(b => b.ArchivedMatchPlayers)
                .HasForeignKey(a => a.PlayerId)
                .IsRequired();
                 


            modelBuilder.Entity<ArchivedMatchPlayers>()
                .HasOne(a => a.archivedMatches)
                .WithMany(b => b.ArchivedMatchPlayers)
                .HasForeignKey(a => a.MatchId)
                .IsRequired();
            

            modelBuilder.Entity<ArchivedMatchPlayers>()
               .Property(p => p.TournamentId)
               .IsRequired();

            modelBuilder.Entity<Tournament>()
                .HasOne(a => a.ArchivedMatchPlayers)
                .WithOne(b => b.Tournament)
                .HasForeignKey<ArchivedMatchPlayers>(b => b.TournamentId);

            //TournamentReference
            modelBuilder.Entity<TournamentReference>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<TournamentReference>()
                .Property(p => p.bodId)
                .IsRequired();

            

            modelBuilder.Entity<Tournament>()
                .HasMany(a => a.TournamentReference)
                .WithOne(b => b.Tournament)
                .HasForeignKey(b => b.tournamentId)
                .IsRequired();

            modelBuilder.Entity<Bot>()
                .HasMany(a => a.TournamentReference)
                .WithOne(b => b.Bot)
                .HasForeignKey(b => b.bodId)
                .IsRequired();
                
            
            // data seed 


        }
    }
}
