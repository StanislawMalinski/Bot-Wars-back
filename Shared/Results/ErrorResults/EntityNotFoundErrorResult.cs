using Shared.Results.IResults;

namespace Shared.Results.ErrorResults;

public class EntityNotFoundErrorResult : INotificationResult , IErrorResult
{
    public string Title { get; init; } = "Entity not found";
    public string Message { get; init; } = "Nie istniej obiekt do którego chcesz się dowołać";
}