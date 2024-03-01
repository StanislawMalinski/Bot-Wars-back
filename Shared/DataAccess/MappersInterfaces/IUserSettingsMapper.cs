using Shared.DataAccess.DTO;
using Shared.DataAccess.DataBaseEntities;

namespace Shared.DataAccess.Mappers;

public interface IUserSettingsMapper
{
    public UserSettings DtoToUserSettings(UserSettingsDto dto);
    public UserSettingsDto UserSettingsToDTO(UserSettings tournament);
}