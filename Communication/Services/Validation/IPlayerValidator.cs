namespace Communication.Services.Validation
{
	public interface IPlayerValidator
	{
		public PlayerPermitEnum ValidateUser(string login, string key);
	}
}
