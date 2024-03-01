using Shared.Results.IResults;

namespace Shared.Results.ErrorResults;

public class IncorrectOperation : INotificationResult, IErrorResult
{
    public string Title { get; init; } = "Incorrect Operation";
    public string Message { get; init; } = "Wykonanie takiej operacji jest nieprawidłowe";
}