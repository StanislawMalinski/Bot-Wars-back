using Shared.Results.IResults;

namespace Shared.Results.ErrorResults;

public class NotTournamentCreatorError : INotificationResult, IErrorResult
{
    public string Title { get; init; }
    public string? Message { get; init; }
}