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

        public async Task<ServiceResponse<FileDto>> GetFile(long id)
        {
            try
            {
                var fileData = await _db.Files.FindAsync(id);
                if (fileData == null)
                {
                    Console.WriteLine("File info not in database");
                    return new ServiceResponse<FileDto>()
                    {
                        Success = false,
                        Message = "File info not in database"
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
                byte[] fileBytes = await File.ReadAllBytesAsync(filePath);
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
                using (var stream = new FileStream(filePath, FileMode.Create))
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
