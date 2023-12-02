namespace BusinessLogic.ExternalProgramServices.Constrain.ConcreteConstrain
{
	public class BotFile : IFileData
	{
		public long Id { get; set; }
		public string? Description { get; set; }
		public string? Filename { get; set; }
		public byte[]? Data { get; set; }

	}
}
