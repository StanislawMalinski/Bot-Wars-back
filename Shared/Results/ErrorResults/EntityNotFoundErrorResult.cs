using Shared.Results.IResults;

namespace Shared.Results.ErrorResults;

public class EntityNotFoundErrorResult : INotificationResult , IErrorResult
{
    public string Title { get; init; } = string.Empty;
    public string Message { get; init; } = string.Empty;
}