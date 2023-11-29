using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.DataAccess.DataBaseEntities;

namespace Shared.DataAccess.EntitiesConfigurations;

public class ArchivedMatchPlayersConfigurations : IEntityTypeConfiguration<ArchivedMatchPlayers>
{
    public void Configure(EntityTypeBuilder<ArchivedMatchPlayers> builder)
    {
        
        builder.HasKey(entity => entity.Id);

        builder.HasOne(x => x.Player)
            .WithMany(x => x.ArchivedMatchPlayers)
            .HasForeignKey(x => x.PlayerId);
        
        

        builder.HasOne(x => x.archivedMatches)
            .WithMany(x => x.ArchivedMatchPlayers)
            .HasForeignKey(x => x.MatchId);


        builder.HasOne(x => x.Tournament)
            .WithOne(x => x.ArchivedMatchPlayers)
            .HasForeignKey<ArchivedMatchPlayers>(x => x.TournamentId);


    }
}