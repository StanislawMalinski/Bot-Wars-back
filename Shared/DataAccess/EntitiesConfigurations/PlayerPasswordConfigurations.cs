using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.DataAccess.DataBaseEntities;

namespace Shared.DataAccess.EntitiesConfigurations;

public class PlayerPasswordConfigurations : IEntityTypeConfiguration<PlayerPassword>
{
    public void Configure(EntityTypeBuilder<PlayerPassword> builder)
    {
        builder.HasKey(entity => entity.Id);

        builder.HasOne(x => x.Player)
            .WithOne(x => x.PlayerPassword)
            .HasForeignKey<PlayerPassword>(x => x.PlayerId);

        builder.Property(entity => entity.HashedPassword)
            .IsRequired();
    }
}