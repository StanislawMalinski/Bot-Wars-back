namespace BotWars.Services.GameTypeService
{
    public interface IPlayerValidator
    {
        public PlayerPermitEnum ValidateUser(string login, string key);
    }
}
