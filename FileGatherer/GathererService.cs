namespace FileGatherer
{
    public class GathererService
    {
        private readonly Database _db;
        private readonly string storagePath = "./Storage/";

        public GathererService(Database db)
        {
            _db = db;
        }

        public async Task<ServiceResponse<bool>> DeleteFile(long id)
        {
            try
            {
                var fileData = await _db.Files.FindAsync(id);
                if (fileData == null)
                {
                    Console.WriteLine($"Delete: File {id} info not in database");
                    return new ServiceResponse<bool>()
                    {
                        Message = $"File {id} info not in database"
                    };
                }
                string filePath = fileData.Path;
                if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
                {
                    Console.WriteLine("File not found");
                    return new ServiceResponse<bool>()
                    {
                        Success = false,
                        Message = "File not found"
                    };
                }
                fileData.Deleted = true;
                await _db.SaveChangesAsync();
                File.Delete(filePath);
                _db.Files.Remove(fileData);
                await _db.SaveChangesAsync();
                return new ServiceResponse<bool>()
                {
                    Success = true,
                    Data = true,
                    Message = $"File {id} deleted successfully"
                };
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return new ServiceResponse<bool>()
                {
                    Message = ex.Message
                };
            }
        }

        public async Task<ServiceResponse<FileDto>> GetFile(long id)
        {
            try
            {
                var fileData = await _db.Files.FindAsync(id);
                if (fileData == null)
                {
                    Console.WriteLine($"Get: File {id} info not in database");
                    return new ServiceResponse<FileDto>()
                    {
                        Success = false,
                        Message = $"File {id} info not in database"
                    };
                }
                string filePath = fileData.Path;
                if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
                {
                    Console.WriteLine("File not found");
                    return new ServiceResponse<FileDto>()
                    {
                        Success = false,
                        Message = "File not found"
                    };
                }
                if (fileData.Deleted)
                {
                    Console.WriteLine("File was previously deleted");
                    File.Delete(filePath);
                    return new ServiceResponse<FileDto>()
                    {
                        Success = false,
                        Message = $"File {id} was previously deleted"
                    };
                }
                byte[] fileBytes;
                using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Delete))
                {
                    fileBytes = new byte[stream.Length];
                    await stream.ReadAsync(fileBytes, 0, (int)stream.Length);
                }
                return new ServiceResponse<FileDto>()
                {
                    Success = true,
                    Data = new FileDto()
                    {
                        Name = fileData.FileName,
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

        public async Task<ServiceResponse<long>> SaveFile(IFormFile file)
        {
            try
            {
                string filePath;
                do
                {
                    filePath = storagePath + Guid.NewGuid().ToString();
                }
                while (File.Exists(filePath));
                using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    await file.CopyToAsync(stream);
                }
                FileData data = new FileData()
                {
                    Path = filePath,
                    FileName = file.FileName
                };
                _db.Files.Add(data);
                _db.SaveChanges();
                return new ServiceResponse<long>()
                {
                    Success = true,
                    Data = data.Id
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<long>()
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }
    }
}
