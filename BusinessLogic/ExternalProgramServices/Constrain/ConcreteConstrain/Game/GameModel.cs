namespace BusinessLogic.ExternalProgramServices.Constrain.ConcreteConstrain.Game
{
	public class GameModel
	{
		public long Id { get; set; }
		public int NumberOfPlayers { get; set; }
		public DateTime LastModification { get; set; }
		public bool IsAvailableForPlay { get; set; }
		public string InterfaceDefinition { get; set; }
		public string GameInstruction { get; set; }
		public string GameFile { get; set; }

	}
}
