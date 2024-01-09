using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.DataAccess.DataBaseEntities;

namespace Shared.DataAccess.EntitiesConfigurations;

public class AchievementThresholdConfigurations : IEntityTypeConfiguration<AchievementThresholds>
{
    public void Configure(EntityTypeBuilder<AchievementThresholds> builder)
    {
        builder.HasKey(entity => entity.Id);
        builder.Property(x => x.Threshold)
            .IsRequired();

        builder.HasOne(x => x.AchievementType)
            .WithMany(x => x.AchievementThresholds)
            .HasForeignKey(x => x.AchievementTypeId)
            .IsRequired();
    }
}