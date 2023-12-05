using Shared.Results.IResults;

namespace Shared.Results;

public class HandlerResult<TSuccessResult, TErrorResult>
    where TSuccessResult : ISuccessResult
    where TErrorResult : IErrorResult
{
    private readonly TSuccessResult? _successResult;
    private readonly TErrorResult? _errorResult;

    public bool IsError { get; init; }
    public bool IsSuccess => !IsError;
    
    public HandlerResult(TSuccessResult successResult)
    {
        IsError = false;
        _successResult = successResult;
        _errorResult = default;
    }
    
    public HandlerResult(TErrorResult errorResult)
    {
        IsError = true;
        _successResult = default;
        _errorResult = errorResult;
    }
    
    public static implicit operator HandlerResult<TSuccessResult, TErrorResult>(TSuccessResult successResult) => new(successResult);
    public static implicit operator HandlerResult<TSuccessResult, TErrorResult>(TErrorResult errorResult) => new(errorResult);

    public TResult Match<TResult>(
        Func<TSuccessResult, TResult> successFunc,
        Func<TErrorResult, TResult> errorFunc) => IsSuccess
        ? successFunc(_successResult ?? throw new InvalidOperationException("Success result is null"))
        : errorFunc(_errorResult ?? throw new InvalidOperationException("Error result is null"));
    
    public async Task<TResult> MatchAsync<TResult>(
        Func<TSuccessResult, Task<TResult>> successFunc,
        Func<TErrorResult, Task<TResult>> errorFunc) => IsSuccess
        ? await successFunc(_successResult ?? throw new InvalidOperationException("Success result is null"))
        : await errorFunc(_errorResult ?? throw new InvalidOperationException("Error result is null"));
}