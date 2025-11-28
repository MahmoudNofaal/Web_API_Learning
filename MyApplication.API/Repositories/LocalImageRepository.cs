using MyApplication.API.Data;
using MyApplication.API.Models.Domain;
using MyApplication.API.Repositories.Interfaces;

namespace MyApplication.API.Repositories;

public class LocalImageRepository : IImageRepository
{
   private readonly IWebHostEnvironment _webHostEnvironment;
   private readonly IHttpContextAccessor _httpContextAccessor;
   private readonly ApplicationDbContext _dbContext;

   public LocalImageRepository(IWebHostEnvironment webHostEnvironment,
                               IHttpContextAccessor httpContextAccessor,
                               ApplicationDbContext dbContext)
   {
      this._webHostEnvironment = webHostEnvironment;
      this._httpContextAccessor = httpContextAccessor;
      this._dbContext = dbContext;
   }


   public async Task<Image> Upload(Image image)
   {
      var localFilePath = Path.Combine(_webHostEnvironment.ContentRootPath,
                                       "wwwroot/images",
                                       $"{image.FileName}{image.FileExtension}");

      // Upload Image to Local Path
      using var stream = new FileStream(localFilePath, FileMode.OpenOrCreate);

      await image.File.CopyToAsync(stream);

      // https://localhost:1710/images/image.jpg

      var urlFilePath = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}{_httpContextAccessor.HttpContext.Request.PathBase}/{image.FileName}{image.FileExtension}";

      image.FilePath = urlFilePath;

      // Add Image to the Images table
      await _dbContext.Images.AddAsync(image);
      await _dbContext.SaveChangesAsync();

      return image;
   }

}
