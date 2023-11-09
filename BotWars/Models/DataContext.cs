using BotWars.GameTypeData;
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
        public DbSet<RockPaperScissors> RockPaperScissors { get; set; }
        public DbSet<GameType> GameTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // fluent api 

            modelBuilder.Entity<Game>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Game>()
                .Property(p => p.Description)
                .IsRequired();

            modelBuilder.Entity<Game>()
                .Property(p => p.Filename)
                .IsRequired();

            modelBuilder.Entity<Game>()
                 .Property(p => p.Data)
                 .IsRequired()
                 .HasColumnType("BLOB");


            modelBuilder.Entity<RockPaperScissors>()
                .ToTable("RockPaperScissors", "Games");

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


            modelBuilder.Entity<GameType>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<GameType>()
                .Property(p => p.Name)
                .IsRequired();

            modelBuilder.Entity<GameType>()
                .Property(p => p.IsAvialable)
                .IsRequired();
            // data seed 


        }
    }
}
