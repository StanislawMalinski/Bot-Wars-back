using Microsoft.AspNetCore.Http;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;
using Shared.Results;
using Shared.DataAccess.DTO.Requests;

namespace Shared.DataAccess.RepositoryInterfaces
{
    public interface IFileRepository
    {
        // name could be anything you want xdd
        public Task<HandlerResult<SuccessData<IFormFile>, IErrorResult>> GetFile(long id, string name);
        public Task<HandlerResult<SuccessData<long>, IErrorResult>> UploadFile(IFormFile file);
        public Task<HandlerResult<SuccessData<string>, IErrorResult>> FormFileToString(IFormFile formFile);
        public HandlerResult<SuccessData<IFormFile>, IErrorResult> StringToFormFile(string content, string fileName, string contentType);
    }
}
