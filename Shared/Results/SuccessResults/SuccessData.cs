using Shared.Results.IResults;

namespace Shared.Results.SuccessResults;

public class SuccessData<T> : ISuccessResult
{
    public T? Data { get; init; }
    
}