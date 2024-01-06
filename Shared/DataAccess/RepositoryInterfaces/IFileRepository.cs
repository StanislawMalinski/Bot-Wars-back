using Shared.Results;
using Shared.Results.IResults;
using Microsoft.AspNetCore.Http;
using Shared.Results.SuccessResults;

namespace Shared.DataAccess.RepositoryInterfaces;

public interface IFileRepository
{
    public Task<HandlerResult<SuccessData<IFormFile>,IErrorResult>> GetFile(string path, string userPermit);
    public Task<HandlerResult<SuccessData<string>,IErrorResult>> PostFile(string name,IFormFile file, string userPermit);
}