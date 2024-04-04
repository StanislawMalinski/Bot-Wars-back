using Shared.Results.IResults;

namespace Shared.Results.ErrorResults;

public class NotBotCreatorError: INotificationResult, IErrorResult
{
    public string Title { get; init; }
    public string? Message { get; init; } 
}
