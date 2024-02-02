using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Shared.DataAccess.Context;
using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.RepositoryInterfaces;
using Shared.Results;
using Shared.Results.ErrorResults;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;

namespace Shared.DataAccess.Repositories;

public class FileRepository : IFileRepository
{
    private readonly DataContext _dataContext;
    private readonly FileCompressor _fileCompressor;

    public FileRepository(DataContext dataContext, FileCompressor fileCompressor)
    {
        _dataContext = dataContext;
        _fileCompressor = fileCompressor;
    }

    public async Task<HandlerResult<SuccessData<IFormFile>, IErrorResult>> GetFile(string path, string userPermit)
    {
        var fileBytes = await _dataContext.Files.FirstOrDefaultAsync(f => f.FilePath == path);

        if (fileBytes == null)
        {
            return new EntityNotFoundErrorResult()
            {
                Title = "return null",
                Message = $"Nie znaleziono Pliku o nazwie {path} w bazie danych"
            };
        }

        var decompressedFile = _fileCompressor.Decompress(fileBytes.FileContent);

        if (decompressedFile == null)
        {
            return new FileUnableToCompressError()
            {
                Title = "return null",
                Message = $"Plik o nazwie {path} napotkal problem podczas dekompresji"
            };
        }

        return new SuccessData<IFormFile>()
        {
            Data = decompressedFile
        };
    }

    public async Task<HandlerResult<SuccessData<string>, IErrorResult>> PostFile(string name, IFormFile file, string userPermit)
    {
        //check if makes sense to pass file and its name, name of the file can be retrieved from itself
        var createdPath = FileNameCreator.GetFilePath(name is null or "" ? file.FileName : name );
        
        var existingFile = await _dataContext.Files.FirstOrDefaultAsync(f => f.FilePath == createdPath);

        if (existingFile != null)
        {
            return new FileUnableToCompressError()
            {
                Title = "return null",
                Message = $"Plik o sciezce {createdPath} istnieje juz w bazie danych"
            };
        }

        var compressedFile = _fileCompressor.Compress(file);

        if (compressedFile == null)
        {
            return new FileUnableToCompressError()
            {
                Title = "return null",
                Message = $"Blad podczas kompresji pliku {name}"
            };
        }

        var newFile = new FileEntity()
        {
            FilePath = createdPath,
            FileContent = compressedFile
        };

        _dataContext.Files.Add(newFile);
        await _dataContext.SaveChangesAsync();

        return new SuccessData<string>()
        {
            Data = createdPath
        };
    }

    public static class FileNameCreator
    {
        private static readonly Dictionary<string, string?> FileTypeExtensions = new()
        {
            { ".jpg", "Image" },
            { ".png", "Image" },
            { ".docx", "Document" },
            { ".pdf", "Document" },
            { ".mp4", "Video" },
            { ".mov", "Video" },
        };

        public static string GetFilePath(string fileName)
        {
            string extension = Path.GetExtension(fileName);

            if (FileTypeExtensions.TryGetValue(extension, out string? folderName))
            {
                return $"{folderName}/{fileName}";
            }
            
            return $"others/{fileName}";
        }
    }
}