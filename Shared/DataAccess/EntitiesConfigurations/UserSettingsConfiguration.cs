using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.DataAccess.DataBaseEntities;

namespace Shared.DataAccess.EntitiesConfigurations;

public class UserSettingsConfiguration : IEntityTypeConfiguration<UserSettings>
{
    public void Configure(EntityTypeBuilder<UserSettings> builder)
    {
        throw new NotImplementedException();
    }
}