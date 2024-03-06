using Microsoft.EntityFrameworkCore.Diagnostics;

namespace FileGatherer
{
    public class GathererService
    {
        private readonly string storagePath = "./Storage/";
        private readonly string gamePath = "Game/";
        private readonly string botPath = "Bot/";

        public async Task<ServiceResponse<bool>> SaveGameFile(IFormFile file)
        {
            return await SaveFile(file, storagePath + gamePath + file.FileName);
        }

        public async Task<ServiceResponse<bool>> SaveBotFile(IFormFile file)
        {
            return await SaveFile(file, storagePath + botPath + file.FileName);
        }

        public async Task<ServiceResponse<FileDto>> GetGameFile(string name)
        {
            if (!ValidateName(name)) {
                return new ServiceResponse<FileDto>()
                {
                    Success = false,
                    Message = "Invalid filename"
                };
            }
            return await GetFile(storagePath + gamePath + name);
        }

        public async Task<ServiceResponse<FileDto>> GetBotFile(string name)
        {
            if (!ValidateName(name))
            {
                return new ServiceResponse<FileDto>()
                {
                    Success = false,
                    Message = "Invalid filename"
                };
            }
            return await GetFile(storagePath + botPath + name);
        }

        private bool ValidateName(string name)
        {
            return !(name.Contains("/") || name.Contains("\\"));
        }

        private async Task<ServiceResponse<FileDto>> GetFile(string filePath)
        {
            try
            {
                if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
                {
                    Console.WriteLine("File not found");
                    return new ServiceResponse<FileDto>()
                    {
                        Success = false,
                        Message = "File not found"
                    };
                }
                byte[] fileBytes = await File.ReadAllBytesAsync(filePath);
                return new ServiceResponse<FileDto>()
                {
                    Success = true,
                    Data = new FileDto()
                    {
                        Content = fileBytes
                    }
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return new ServiceResponse<FileDto>()
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<ServiceResponse<bool>> SaveFile(IFormFile file, string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    return new ServiceResponse<bool>()
                    {
                        Success = false,
                        Message = $"File {filePath} already exists"
                    };
                }
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                return new ServiceResponse<bool>()
                {
                    Success = true,
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<bool>()
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }
    }
}
