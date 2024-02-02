using Communication.ServiceInterfaces;
using Microsoft.AspNetCore.Http;
using Shared.DataAccess.RepositoryInterfaces;
using Shared.Results;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Communication.Services.FIle;

public class FileProvider : IFileProvider
{
    private readonly IFileRepository _repository;

    public FileProvider(IFileRepository repository)
    {
        _repository = repository;
    }
    public async Task<HandlerResult<SuccessData<IFormFile>, IErrorResult>> GetFile(string path, string userPermit)
    {
        return await _repository.GetFile(path, userPermit);
    }

    public async Task<HandlerResult<SuccessData<string>, IErrorResult>> PostFile(string name, IFormFile file, string userPermit)
    {
        return await _repository.PostFile(name, file, userPermit);
    }
}