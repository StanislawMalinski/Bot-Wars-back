using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.DataAccess.DataBaseEntities;

namespace Shared.DataAccess.EntitiesConfigurations;

public class TournamentReferenceConfigurations : IEntityTypeConfiguration<TournamentReference>
{
    public void Configure(EntityTypeBuilder<TournamentReference> builder)
    {
        builder.HasKey(entity => entity.Id);

        builder.Property(entity => entity.bodId)
            .IsRequired();

        builder.HasOne(x => x.Tournament)
            .WithMany(x => x.TournamentReference)
            .HasForeignKey(x => x.tournamentId);


        builder.HasOne(x => x.Bot)
            .WithMany(x => x.TournamentReference)
            .HasForeignKey(x => x.bodId);
        
        builder.Property(entity => entity.tournamentId)
            .IsRequired();

        
    }
}