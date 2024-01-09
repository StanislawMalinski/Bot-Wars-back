using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.DataAccess.DataBaseEntities;

namespace Shared.DataAccess.EntitiesConfigurations;

public class AchievementRecordConfigurations : IEntityTypeConfiguration<AchievementRecord>
{
    public void Configure(EntityTypeBuilder<AchievementRecord> builder)
    {
        builder.HasKey(entity => entity.Id);

        builder.HasOne(x => x.AchievementType)
            .WithMany(x => x.AchievementRecords)
            .HasForeignKey(x => x.AchievementTypeId)
            .IsRequired();

        builder.HasOne(x => x.Player)
            .WithMany(x => x.AchievementRecords)
            .HasForeignKey(x => x.PlayerId);

        builder.Property(x => x.Value)
            .IsRequired();
        
        
    }
}