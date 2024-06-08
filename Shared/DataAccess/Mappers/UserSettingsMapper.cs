using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.DTO;

namespace Shared.DataAccess.Mappers;

public class UserSettingsMapper : IUserSettingsMapper
{
    public UserSettings DtoToUserSettings(UserSettingsDto dto)
    {
        return new UserSettings
        {
            IsDarkTheme = dto.IsDarkTheme,
            Language = dto.Language
        };
    }

    public UserSettingsDto UserSettingsToDTO(UserSettings userSettings)
    {
        return new UserSettingsDto
        {
            IsDarkTheme = userSettings.IsDarkTheme,
            Language = userSettings.Language
        };
    }
}