namespace BusinessLogic.ExternalProgramServices.Constrain.ConcreteConstrain
{
	public interface IFileData
	{
		public string? Filename { get; set; }
		public byte[]? Data { get; set; }
	}
}
