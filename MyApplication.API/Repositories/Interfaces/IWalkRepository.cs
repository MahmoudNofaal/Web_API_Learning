using MyApplication.API.Models.Domain;

namespace MyApplication.API.Repositories.Interfaces;

public interface IWalkRepository
{
   Task<Walk> CreateAsync(Walk walk);

   Task<List<Walk>> GetAllAsync(string? filterOn = null,
                                string? filterQuery = null,
                                string? sortBy = null,
                                bool? isAscending = null,
                                int pageNumber = 1,
                                int pageSize = 5);

   Task<Walk?> GetByIdAsync(Guid id);

   Task<Walk?> UpdateAsync(Guid id, Walk walk);

   Task<Walk?> DeleteAsync(Guid id);
}
