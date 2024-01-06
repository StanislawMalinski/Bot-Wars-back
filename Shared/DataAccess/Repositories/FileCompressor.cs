using System;
using System.IO;
using System.IO.Compression;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;

namespace Shared.DataAccess.Repositories
{
    public class FileCompressor
    {
        public byte[]? Compress(IFormFile file)
        {
            try
            {
                using var memoryStream = new MemoryStream();
                using var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true);

                var zipEntry = archive.CreateEntry(file.FileName);

                using var entryStream = zipEntry.Open();
                file.CopyTo(entryStream);

                return memoryStream.ToArray();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public IFormFile? Decompress(byte[]? file)
        {
            try
            {
                using var memoryStream = new MemoryStream(file);
                using var archive = new ZipArchive(memoryStream);

                if (archive.Entries.Count == 0)
                {
                    return null;
                }

                var zipEntry = archive.Entries[0];

                using var entryStream = zipEntry.Open();
                using var ms = new MemoryStream();
                entryStream.CopyTo(ms);

                var fileName = Path.GetFileName(zipEntry.FullName);

                var formFile = new FormFile(ms, 0, ms.Length, null, fileName)
                {
                    Headers = new HeaderDictionary(),
                    ContentType = "application/octet-stream"
                };

                return formFile;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
