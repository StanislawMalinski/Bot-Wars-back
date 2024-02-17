using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.DataAccess.DataBaseEntities;

namespace Shared.DataAccess.EntitiesConfigurations;

public class AchievementTypeConfigurations : IEntityTypeConfiguration<AchievementType>
{
    public void Configure(EntityTypeBuilder<AchievementType> builder)
    {
        builder.HasKey(entity => entity.Id);
        builder.Property(x => x.Description)
            .HasMaxLength(100)
            .IsRequired();
    }
}