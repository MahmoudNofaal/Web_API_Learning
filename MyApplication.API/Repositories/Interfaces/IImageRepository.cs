using MyApplication.API.Models.Domain;

namespace MyApplication.API.Repositories.Interfaces;

public interface IImageRepository
{
   Task<Image> Upload(Image image);
}
