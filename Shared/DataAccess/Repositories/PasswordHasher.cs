using Shared.DataAccess.RepositoryInterfaces;
using Shared.Results;
using Shared.Results.ErrorResults;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Shared.DataAccess.Repositories;

public class PasswordHasher : IPasswordHasher
{
    
    private readonly int workFacktor = 12;
    
    public async Task<HandlerResult<SuccessData<string>, IErrorResult>> HashPassword(string password)
    {
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(password, workFacktor);
        return new SuccessData<string>()
        {
            Data = passwordHash
        };
    }

    public async Task<HandlerResult<Success, IErrorResult>> VerifyPasswordHash(string password, string passwordHash)
    {
        var result = BCrypt.Net.BCrypt.Verify(password, passwordHash);
        if (result)
        {
            return new Success();
        }
        else
        {
            return new EntityNotFoundErrorResult()
            {
                Title = "Return null",
                Message = "Niepoprawne hasło"
            };
        }
    }
}