using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.DataAccess.DataBaseEntities;

namespace Shared.DataAccess.EntitiesConfigurations;

public class PlayerConfigurations : IEntityTypeConfiguration<Player>
{
    public void Configure(EntityTypeBuilder<Player> builder)
    {
        
        builder.HasKey(entity => entity.Id);

        builder.Property(entity => entity.Email)
            .IsRequired()
            .HasMaxLength(30);

        builder.Property(entity => entity.Login)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(entity => entity.Points)
            .IsRequired();
        

        builder.Property(entity => entity.HashedPassword)
            .IsRequired();
        

        // builder.HasOne(entity => entity.Role)
        //     .WithMany()
        //     .HasForeignKey(p => p.RoleId);
    }
}