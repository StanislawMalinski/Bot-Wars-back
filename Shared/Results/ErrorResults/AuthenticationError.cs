using Shared.Results.IResults;

namespace Shared.Results.ErrorResults;

public class AuthenticationError : INotificationResult, IErrorResult
{
    public string Title { get; init; } = "Not authorized";
    public string Message { get; init; } = "Nie posiadasz uprawnień do wykonania tej operacji";
}