using Communication.ServiceInterfaces;

namespace Communication.Services.UserSettings;

public class UserSettingsAdminService :  UserSettingsIdentifiedPlayerService ,IUserSettingsService
{

    public UserSettingsAdminService(UserSettingsServiceProvider userSettingsServiceProvider) : base(userSettingsServiceProvider)
    {
    }
}