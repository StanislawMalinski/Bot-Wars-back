// using BotWars.Services;
// using Microsoft.AspNetCore.Http;
// using Microsoft.AspNetCore.Mvc;
// using Shared.DataAccess.RepositoryInterfaces;
// namespace Communication.APIs.Controllers;
//
// [Route("api/file")]
// [ApiController]
// public class FileSenderController : ControllerBase
// {
//     private readonly IFileService _fileService;
//
//     public FileSenderController(IFileService fileService)
//     {
//         _fileService = fileService;
//     }
//     
//     [HttpGet("{path}")]
//     public async Task<ActionResult<ServiceResponse<IFormFile>>> GetFile(string path)
//     {
//         var response = await _fileService.GetFile(path);
//         
//         if (response.Success)
//         {
//             return Ok(response);
//         }
//         
//         return NotFound(response.Message);
//             
//     }
//     
//     // [HttpGet]
//     // public async Task<ActionResult<ServiceResponse<List<IFormFile>>>> GetFiles()
//     // {
//     //     var response = await _fileService.GetFiles(path);
//     //     
//     //     if (response.Success)
//     //     {
//     //         return Ok(response);
//     //     }
//     //     
//     //     return NotFound(response.Message);
//     // }
//     
//     [HttpPost]
//     public async Task<ActionResult<ServiceResponse<string>>> UploadFile(IFormFile file)
//     {
//         var response = await _fileService.PostFile(file);
//         
//         if (response.Success)
//         {
//             return Ok(response);
//         }
//         
//         return NotFound(response.Message);
//     }
// }