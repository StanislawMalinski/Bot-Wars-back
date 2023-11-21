namespace BotWars.Services.GameTypeService
{
    public interface IPlayerValidator
    {
        int ValidateUser(String login, String key);
    }
}
