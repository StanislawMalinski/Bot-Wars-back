namespace BusinessLogic.ExternalProgramServices.niewiemcoztymzrobic
{
	public partial interface IGameManager
	{
		// rather useless interface
		public void PlayGame(ProgramInstance game, List<ProgramInstance> bots);
		public void HandleOutput(int processId, string output);
		public void HandleExit(int processId);
	}
}
