using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyApplication.API.Models.Domain;
using MyApplication.API.Models.DTOs;
using MyApplication.API.Repositories.Interfaces;
using System.Threading.Tasks;

namespace MyApplication.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ImagesController : ControllerBase
{
   private readonly IImageRepository _imageRepository;

   public ImagesController(IImageRepository imageRepository)
   {
      this._imageRepository = imageRepository;
   }

   [HttpPost("upload")]
   public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto dto )
   {
      ValidateFileUpload(dto);

      if (ModelState.IsValid)
      {
         var image = new Image()
         {
            File = dto.File,
            FileName = dto.FileName,
            FileDescription = dto.FileDescription,
            FileExtension = Path.GetExtension(dto.File.FileName).ToLower(),
            FileSizeInBytes = dto.File.Length,
         };

         // get the path of the file
         var path = await _imageRepository.Upload(image);


         return Ok(image);
      }

      return BadRequest(ModelState);
   }

   private void ValidateFileUpload(ImageUploadRequestDto dto)
   {
      var allowedExtensions = new List<string> { ".jpg", ".jpeg", ".png" };

      var maxFileSizeInBytes = 5 * 1024 * 1024; // 5 MB

      var fileExtension = Path.GetExtension(dto.File.FileName).ToLower();

      if (!allowedExtensions.Contains(fileExtension))
      {
         ModelState.AddModelError("File", "Unsupported file type. Allowed types are: .jpg, .jpeg, .png");
      }

      if (dto.File.Length > maxFileSizeInBytes)
      {
         ModelState.AddModelError("File", "File size exceeds the maximum limit of 5 MB.");
      }

   }

}

