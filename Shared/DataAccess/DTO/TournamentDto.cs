using System.ComponentModel.DataAnnotations;
using Shared.validation;

namespace Shared.DataAccess.DTO
{
	public class TournamentDto
	{
		public long Id { get; set; }
		public string TournamentsTitle { get; set; }
		public string Description { get; set; }
		[Required]
		public long GameId { get; set; }
		public int PlayersLimit { get; set; }
		[Required(ErrorMessage = "Date is required")]
		[FutureDate(ErrorMessage = "Date must be at least the 24 hours later")]
		public DateTime TournamentsDate { get; set; }
		public DateTime PostedDate { get; set; }
		public bool WasPlayedOut { get; set; }
		public string? Constrains { get; set; }
		public string? Image { get; set; }
	}
}
