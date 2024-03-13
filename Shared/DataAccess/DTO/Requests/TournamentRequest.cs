using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Shared.DataAccess.Enumerations;
using Shared.validation;

namespace Shared.DataAccess.DTO.Requests;

public class TournamentRequest
{
    [NotNull]
    public string? TournamentTitle { get; set; }
    [NotNull]
    public string? Description { get; set; }
    public long GameId { get; set; }
    public int PlayersLimit { get; set; }
    [Required(ErrorMessage = "Date is required")]
    [FutureDate(ErrorMessage = "Date must be at least the 24 hours later")]
    public DateTime TournamentsDate { get; set; }
    [Range(0, 1)]
    public RankingTypes RankingType { get; set; }
    [NotNull]
    public string? Constraints { get; set; }
    [NotNull]
    public string? Image { get; set; }
}