using BotWars.Services.GameTypeService;
using BotWars.Services.IServices;

namespace BotWars.Services;

public class FileService : IFileService
{
    private readonly IPlayerValidator _playerValidator;

    public FileService(IPlayerValidator playerValidator)
    {
        _playerValidator = playerValidator;
    }
    
    public async Task<ServiceResponse<IFormFile>> GetFile(string path)
    {
        throw new NotImplementedException();
    }

    public async Task<ServiceResponse<List<IFormFile>>> GetFiles(string path)
    {
        throw new NotImplementedException();
    }

    public async Task<ServiceResponse<string>> PostFile(IFormFile file)
    {
        throw new NotImplementedException();
    }

    public async Task<ServiceResponse<List<string>>> PostFiles(List<IFormFile> files)
    {
        throw new NotImplementedException();
    }
}