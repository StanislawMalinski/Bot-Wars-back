using Shared.DataAccess.DAO;
using Shared.DataAccess.DataBaseEntities;

namespace Shared.DataAccess.Mappers;

public class UserSettingsMapper : IUserSettingsMapper
{
    public UserSettings DtoToUserSettings(UserSettingsDto dto)
    {
        return new UserSettings
        {
            PlayerId = dto.PlayerId,
            IsDarkTheme = dto.IsDarkTheme,
            Language = dto.Language
        };
    }

    public UserSettingsDto UserSettingsToDTO(UserSettings userSettings)
    {
        return new UserSettingsDto
        {
            PlayerId = userSettings.PlayerId,
            IsDarkTheme = userSettings.IsDarkTheme,
            Language = userSettings.Language
        };
    }
}