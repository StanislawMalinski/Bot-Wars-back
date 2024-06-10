using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.DTO;

namespace Shared.DataAccess.Mappers;

public interface IUserSettingsMapper
{
    public UserSettings DtoToUserSettings(UserSettingsDto dto);
    public UserSettingsDto UserSettingsToDTO(UserSettings tournament);
}