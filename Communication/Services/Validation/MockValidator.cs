namespace Communication.Services.Validation
{
	// to be replaced with actual validator
	public class MockValidator : IPlayerValidator
	{
		public PlayerPermitEnum ValidateUser(string login, string key)
		{
			return PlayerPermitEnum.ADMIN;
		}
	}
}
