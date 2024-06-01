using Microsoft.AspNetCore.Http;
using Shared.DataAccess.DataBaseEntities;
using Shared.DataAccess.DTO.Requests;
using Shared.DataAccess.RepositoryInterfaces;
using Shared.Results;
using Shared.Results.ErrorResults;
using Shared.Results.IResults;
using Shared.Results.SuccessResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataAccess.Repositories
{
    public class FileRepository : IFileRepository
    {
        private readonly HttpClient _httpClient;
        private readonly string _gathererEndpoint = "http://file_gatherer:8080/api/Gatherer";
        public FileRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HandlerResult<SuccessData<long>, IErrorResult>> UploadFile(IFormFile file)
        {
            var content = new MultipartFormDataContent();
            var streamContent = new StreamContent(file.OpenReadStream());
            content.Add(streamContent, "file", file.FileName);
            var res = await _httpClient.PutAsync(_gathererEndpoint, content);
            if (res.IsSuccessStatusCode)
            {
                string cont = await res.Content.ReadAsStringAsync();
                long id = Convert.ToInt32(cont);
                return new SuccessData<long>
                {
                    Data = id
                };
            }
            else
            {
                return new IncorrectOperation
                {
                    Title = "IncorrectOperation 404",
                    Message = "Faild to upload file to FileGatherer"
                };
            }
        }

        // name could be anything you want xdd
        public async Task<HandlerResult<SuccessData<IFormFile>, IErrorResult>> GetFile(long id, string name)
        {
            try
            {
                string endpoint = _gathererEndpoint + $"/{id}";
                HttpResponseMessage res = await _httpClient.GetAsync(endpoint);
                if (!res.IsSuccessStatusCode)
                {
                    Console.WriteLine(string.Format(_gathererEndpoint, id));
                    Console.WriteLine(res.StatusCode);
                    Console.WriteLine($"GetFile: file {id} not found in Gatherer");
                    return new EntityNotFoundErrorResult
                    {
                        Title = "EntityNotFoundErrorResult",
                        Message = $"File {id} not found in Gatherer"
                    };
                }

                Stream cont = await res.Content.ReadAsStreamAsync();

                IFormFile resBotFile = new FormFile(cont, 0, cont.Length, "botFile", name)
                {
                    Headers = new HeaderDictionary(),
                    ContentType = "text/plain"
                };

                return new SuccessData<IFormFile>() { Data = resBotFile };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetFile: {ex.Message}");
                return new EntityNotFoundErrorResult
                {
                    Title = "Exception",
                    Message = ex.Message
                };
            }
        }

        public async Task<HandlerResult<SuccessData<string>, IErrorResult>> FormFileToString(IFormFile formFile)
        {
            if (formFile == null)
            {
                return new IncorrectOperation();
            }
            using (var stream = formFile.OpenReadStream())
            using (var reader = new StreamReader(stream))
            {
                string res = await reader.ReadToEndAsync();
                return new SuccessData<string>()
                {
                    Data = res
                };
            }
        }

        public HandlerResult<SuccessData<IFormFile>, IErrorResult> StringToFormFile(string content, string fileName, string contentType)
        {
            try
            {
                byte[] byteArray = Encoding.UTF8.GetBytes(content);
                var stream = new MemoryStream(byteArray);
                IFormFile formFile = new FormFile(stream, 0, stream.Length, "formFile", fileName)
                {
                    Headers = new HeaderDictionary(),
                    ContentType = contentType
                };
                return new SuccessData<IFormFile> { Data = formFile };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"StringToFormFile: {ex.Message}");
                return new EntityNotFoundErrorResult
                {
                    Title = "Exception",
                    Message = ex.Message
                };
            }
        }
    }
}
