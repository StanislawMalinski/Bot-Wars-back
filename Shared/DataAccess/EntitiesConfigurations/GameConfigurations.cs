using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.DataAccess.Context;
using Shared.DataAccess.DataBaseEntities;

namespace Shared.DataAccess.EntitiesConfigurations;

public class GameConfigurations : IEntityTypeConfiguration<Game>
{
    public void Configure(EntityTypeBuilder<Game> builder)
    {
        
        builder.HasKey(entity => entity.Id);

        builder.Property(entity => entity.NumbersOfPlayer)
            .IsRequired();

        builder.Property(entity => entity.LastModification)
            .IsRequired();

        builder.Property(entity => entity.GameFile)
            .IsRequired()
            .HasMaxLength(400);

        builder.Property(entity => entity.GameInstructions)
            .IsRequired()
            .HasMaxLength(400);

        builder.Property(entity => entity.InterfaceDefinition)
            .IsRequired()
            .HasMaxLength(400);

        builder.Property(entity => entity.IsAvailableForPlay)
            .IsRequired();
        
    }
}