using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyApplication.API.CustomActionFilters;
using MyApplication.API.Models.Domain;
using MyApplication.API.Models.DTOs;
using MyApplication.API.Repositories.Interfaces;

namespace MyApplication.API.Controllers;

// /api/walks
[Route("api/[controller]")]
[ApiController]
public class WalksController : ControllerBase
{
   private readonly IMapper _mapper;
   private readonly IWalkRepository _walkRepository;

   public WalksController(IMapper mapper, IWalkRepository walkRepository)
   {
      this._mapper = mapper;
      this._walkRepository = walkRepository;
   }


   // CREATE Walk
   // POST: /api/walks
   [HttpPost]
   [ValidateModel]
   public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
   {
      // Map DTO to Domain Model
      var walkDomainModel = _mapper.Map<Walk>(addWalkRequestDto);

      await _walkRepository.CreateAsync(walkDomainModel);

      var dto = _mapper.Map<WalkDto>(walkDomainModel);

      // Map Domain model to DTO
      return Ok(dto);
   }


   // GET Walks
   // GET: /api/walks?filterOn=Name&filterQuery=Track&sortBy=Name&isAscending=true&pageNumber=1&pageSize=10
   [HttpGet]
   public async Task<IActionResult> GetAll([FromQuery] string? filterOn,
                                           [FromQuery] string? filterQuery,
                                           [FromQuery] string? sortBy,
                                           [FromQuery] bool? isAscending,
                                           [FromQuery] int pageNumber = 1,
                                           [FromQuery] int pageSize = 5)
   {
      var walksDomainModel = await _walkRepository.GetAllAsync(filterOn,
                                                               filterQuery,
                                                               sortBy,
                                                               isAscending,
                                                               pageNumber,
                                                               pageSize);

      // Map Domain Model to DTO
      var dtos = _mapper.Map<List<WalkDto>>(walksDomainModel);

      return Ok(dtos);
   }

   // Get Walk By Id
   // GET: /api/Walks/{id}
   [HttpGet]
   [Route("{id:Guid}")]
   public async Task<IActionResult> GetById([FromRoute] Guid id)
   {
      var walkDomainModel = await _walkRepository.GetByIdAsync(id);

      if (walkDomainModel == null)
      {
         return NotFound();
      }

      // Map Domain Model to DTO
      var dto = _mapper.Map<WalkDto>(walkDomainModel);

      return Ok(dto);
   }

   // Update Walk By Id
   // PUT: /api/Walks/{id}
   [HttpPut]
   [Route("{id:Guid}")]
   [ValidateModel]
   public async Task<IActionResult> Update([FromRoute] Guid id,
                                           UpdateWalkRequestDto updateWalkRequestDto)
   {

      // Map DTO to Domain Model
      var walkDomainModel = _mapper.Map<Walk>(updateWalkRequestDto);

      walkDomainModel = await _walkRepository.UpdateAsync(id, walkDomainModel);

      if (walkDomainModel == null)
      {
         return NotFound();
      }

      // Map Domain Model to DTO
      var dto = _mapper.Map<WalkDto>(walkDomainModel);

      return Ok(dto);
   }


   // Delete a Walk By Id
   // DELETE: /api/Walks/{id}
   [HttpDelete]
   [Route("{id:Guid}")]
   public async Task<IActionResult> Delete([FromRoute] Guid id)
   {
      var deletedWalkDomainModel = await _walkRepository.DeleteAsync(id);

      if (deletedWalkDomainModel == null)
      {
         return NotFound();
      }

      // Map Domain Model to DTO
      var dto = _mapper.Map<WalkDto>(deletedWalkDomainModel);

      return Ok(dto);
   }

}
