using MyApplication.API.Models.Domain;
using MyApplication.API.Repositories.Interfaces;

namespace MyApplication.API.Repositories;

public class InMemoryRegionRepository : IRegionRepository
{
   public Task<Region> CreateAsync(Region region)
   {
      throw new NotImplementedException();
   }

   public Task<Region?> DeleteAsync(Guid id)
   {
      throw new NotImplementedException();
   }

   public async Task<IEnumerable<Region>> GetAllAsync()
   {
      return new List<Region>()
      {
         new Region()
         {
            Id = Guid.NewGuid(),
            Code = "US-CA",
            Name = "California",
            RegionImageUrl = "https://example.com/images/california.png"
         },
         new Region()
         {
            Id = Guid.NewGuid(),
            Code = "US-NY",
            Name = "New York",
            RegionImageUrl = "https://example.com/images/newyork.png"
         },
      };
   }

   public Task<Region?> GetByIdAsync(Guid id)
   {
      throw new NotImplementedException();
   }

   public Task<Region?> UpdateAsync(Guid id, Region region)
   {
      throw new NotImplementedException();
   }

   Task<List<Region>> IRegionRepository.GetAllAsync()
   {
      throw new NotImplementedException();
   }
}
