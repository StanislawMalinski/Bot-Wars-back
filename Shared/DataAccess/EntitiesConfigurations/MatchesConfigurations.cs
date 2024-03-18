using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.DataAccess.DataBaseEntities;

namespace Shared.DataAccess.EntitiesConfigurations;

public class MatchesConfigurations : IEntityTypeConfiguration<Matches>
{
    public void Configure(EntityTypeBuilder<Matches> builder)
    {
        
        builder.HasKey(entity => entity.Id);

        builder.HasOne(x => x.Game)
            .WithMany(x => x.Matches)
            .HasForeignKey(x => x.GameId);

        builder.HasOne(x => x.Tournament)
            .WithMany(x => x.Matches)
            .HasForeignKey(x => x.TournamentsId)
            .OnDelete(DeleteBehavior.ClientSetNull);
        
        builder.Property(entity => entity.Status)
            .IsRequired();
        builder.Property(entity => entity.Winner)
            .IsRequired();
        builder.Property(entity => entity.Data)
            .IsRequired();
        
    }
}