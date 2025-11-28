using Microsoft.EntityFrameworkCore;
using MyApplication.API.Data;
using MyApplication.API.Models.Domain;
using MyApplication.API.Repositories.Interfaces;
using System;

namespace MyApplication.API.Repositories;

public class SQLWalkRepository : IWalkRepository
{
   private readonly ApplicationDbContext _dbContext;

   public SQLWalkRepository(ApplicationDbContext dbContext)
   {
      this._dbContext = dbContext;
   }


   public async Task<Walk> CreateAsync(Walk walk)
   {
      await _dbContext.Walks.AddAsync(walk);
      await _dbContext.SaveChangesAsync();

      return walk;
   }

   public async Task<Walk?> DeleteAsync(Guid id)
   {
      var existingWalk = await _dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);

      if (existingWalk == null)
      {
         return null;
      }

      _dbContext.Walks.Remove(existingWalk);
      await _dbContext.SaveChangesAsync();

      return existingWalk;
   }

   public async Task<List<Walk>> GetAllAsync(string? filterOn = null,
                                             string? filterQuery = null,
                                             string? sortBy = null,
                                             bool? isAscending = null,
                                             int pageNumber = 1,
                                             int pageSize = 5)
   {
      var query = _dbContext.Walks.Include("Difficulty")
                                  .Include("Region")
                                  .AsQueryable();

      // filtering
      if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
      {
         if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
         {
            query = query.Where(x => x.Name.Contains(filterQuery));
         }

      }

      // sorting
      if (string.IsNullOrWhiteSpace(sortBy) == false && isAscending.HasValue)
      {
         if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
         {
            query = isAscending.Value ? query.OrderBy(x => x.Name) : query.OrderByDescending(x => x.Name);
         }
         else if (sortBy.Equals("Length", StringComparison.OrdinalIgnoreCase))
         {
            query = isAscending.Value ? query.OrderBy(x => x.LengthInKm) : query.OrderByDescending(x => x.LengthInKm);
         }

      }

      // pagination
      var skipResults = (pageNumber - 1) * pageSize;

      query = query.Skip(skipResults).Take(pageSize);


      return await query.ToListAsync();
      //return await dbContext.Walks.Include("Difficulty")
      //                            .Include("Region")
      //                            .ToListAsync();
   }

   public async Task<Walk?> GetByIdAsync(Guid id)
   {
      return await _dbContext.Walks.Include("Difficulty")
                                  .Include("Region")
                                  .FirstOrDefaultAsync(x => x.Id == id);
   }

   public async Task<Walk?> UpdateAsync(Guid id, Walk walk)
   {
      var existingWalk = await _dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);

      if (existingWalk == null)
      {
         return null;
      }

      existingWalk.Name = walk.Name;
      existingWalk.Description = walk.Description;
      existingWalk.LengthInKm = walk.LengthInKm;
      existingWalk.WalkImageUrl = walk.WalkImageUrl;
      existingWalk.DifficultyId = walk.DifficultyId;
      existingWalk.RegionId = walk.RegionId;

      await _dbContext.SaveChangesAsync();

      return existingWalk;
   }

}
