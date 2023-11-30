namespace Shared.DataAccess.Services.Results
{
	public class ServiceResponse<T>
	{
		public T? Data { get; set; }

		public bool Success { get; set; }

		public string Message { get; set; }

		// don't know if it should be here
		public static ServiceResponse<T> AccessDeniedResponse()
		{
			return new ServiceResponse<T>()
			{
				Success = false,
				Message = "Access denied"
			};
		}
	}
}
