using BotWars.Gry;
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

            // data seed 


        }
    }
}
