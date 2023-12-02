using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.DataAccess.DataBaseEntities;

namespace Shared.DataAccess.EntitiesConfigurations;

public class ArchivedMatchesConfigurations : IEntityTypeConfiguration<ArchivedMatches>
{
    public void Configure(EntityTypeBuilder<ArchivedMatches> builder)
    {
        
        builder.HasKey(entity => entity.Id);

        builder.HasOne(x => x.Game)
            .WithMany(x => x.ArchivedMatches)
            .HasForeignKey(x => x.GameId);

        builder.HasOne(x => x.Tournament)
            .WithMany(x => x.ArchivedMatches)
            .HasForeignKey(x => x.TournamentsId)
            .OnDelete(DeleteBehavior.ClientSetNull);
        
        builder.Property(entity => entity.Played)
            .IsRequired();
        
        builder.Property(entity => entity.Match)
            .IsRequired();
        
    }
}