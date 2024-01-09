using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.DataAccess.DataBaseEntities;

namespace Shared.DataAccess.EntitiesConfigurations;

public class UserSettingsConfiguration : IEntityTypeConfiguration<UserSettings>
{
    public void Configure(EntityTypeBuilder<UserSettings> builder)
    {
        builder.HasKey(entity => entity.Id);
        builder.HasOne(x => x.Player)
            .WithOne(x => x.UserSettings)
            .HasForeignKey<UserSettings>(x => x.PlayerId);

        builder.Property(x => x.Language)
            .IsRequired();

        builder.Property(x => x.IsDarkTheme)
            .IsRequired();
    }
}