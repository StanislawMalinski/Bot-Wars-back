using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.DataAccess.DataBaseEntities;

namespace Shared.DataAccess.EntitiesConfigurations;

public class TournamentConfigurations : IEntityTypeConfiguration<Tournament>
{
    public void Configure(EntityTypeBuilder<Tournament> builder)
    {
        
        
        builder.HasKey(entity => entity.Id);

        builder.Property(entity => entity.TournamentTitle)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(entity => entity.Description)
            .IsRequired()
            .HasMaxLength(1000);

        builder.HasOne(x => x.Game)
            .WithMany(x => x.Tournaments)
            .HasForeignKey(x => x.GameId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder.Property(entity => entity.PlayersLimit)
            .IsRequired();

        builder.Property(entity => entity.TournamentsDate)
            .IsRequired();

        builder.Property(entity => entity.WasPlayedOut)
            .IsRequired();

        builder.Property(entity => entity.Constraints)
            .IsRequired();
    }
}