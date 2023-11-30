namespace BusinessLogic.ExternalProgramServices.Constrain.ConcreteConstrain.Game
{
	public class MatchResult<T>
	{
		public bool Success { get; set; }
		public T? Data { get; set; }
	}
}
