using Shared.DataAccess.DTO.Requests;
using Shared.Results;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Shared.DataAccess.RepositoryInterfaces;

public interface IPasswordHasher
{
    Task<HandlerResult<SuccessData<string>, IErrorResult>> HashPassword(string password);
    Task<HandlerResult<Success, IErrorResult>> VerifyPasswordHash(string password, string passwordHash);
}