using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.DataAccess.DataBaseEntities;

namespace Shared.DataAccess.EntitiesConfigurations;

public class BotConfigurations : IEntityTypeConfiguration<Bot>
{
    public void Configure(EntityTypeBuilder<Bot> builder)
    {
        builder.HasKey(entity => entity.Id);

        builder.HasOne(x => x.Player)
            .WithMany(x => x.Bot)
            .HasForeignKey(x => x.PlayerId);

        builder.HasOne(x => x.Games)
            .WithMany(x => x.Bot)
            .HasForeignKey(x => x.GameId);

        builder.Property(x => x.BotFile)
            .IsRequired()
            .HasMaxLength(400);

        builder.Property(x => x.Validation)
            .IsRequired();
    }
}