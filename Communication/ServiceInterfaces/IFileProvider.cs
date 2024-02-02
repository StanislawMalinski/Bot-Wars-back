using Microsoft.AspNetCore.Http;
using Shared.Results;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Communication.ServiceInterfaces;

public interface IFileProvider
{
    public Task<HandlerResult<SuccessData<IFormFile>,IErrorResult>> GetFile(string path, string userPermit);
    public Task<HandlerResult<SuccessData<string>,IErrorResult>> PostFile(string name,IFormFile file, string userPermit);
}