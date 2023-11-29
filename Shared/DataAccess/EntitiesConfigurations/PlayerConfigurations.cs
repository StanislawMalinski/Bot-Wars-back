using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.DataAccess.DataBaseEntities;

namespace Shared.DataAccess.EntitiesConfigurations;

public class PlayerConfigurations : IEntityTypeConfiguration<Player>
{
    public void Configure(EntityTypeBuilder<Player> builder)
    {
        
        builder.HasKey(entity => entity.Id);

        builder.Property(entity => entity.email)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(entity => entity.login)
            .IsRequired()
            .HasMaxLength(200);

     
    }
}