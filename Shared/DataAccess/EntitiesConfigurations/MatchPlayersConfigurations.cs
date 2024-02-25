using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.DataAccess.DataBaseEntities;

namespace Shared.DataAccess.EntitiesConfigurations;

public class MatchPlayersConfigurations : IEntityTypeConfiguration<MatchPlayers>
{
    public void Configure(EntityTypeBuilder<MatchPlayers> builder)
    {
        
        builder.HasKey(entity => entity.Id);

        builder.HasOne(x => x.Bot)
            .WithMany(x => x.MatchPlayers)
            .HasForeignKey(x => x.BotId)
            .OnDelete(DeleteBehavior.NoAction);



        builder.HasOne(x => x.Matches)
            .WithMany(x => x.MatchPlayers)
            .HasForeignKey(x => x.MatchId)
            .OnDelete(DeleteBehavior.NoAction);

        /*
        builder.HasOne(x => x.Tournament)
            .WithOne(x => x.ArchivedMatchPlayers)
            .HasForeignKey<ArchivedMatchPlayers>(x => x.TournamentId)
            .OnDelete(DeleteBehavior.Cascade);*/


    }
}