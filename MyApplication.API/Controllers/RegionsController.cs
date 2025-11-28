using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyApplication.API.CustomActionFilters;
using MyApplication.API.Models.Domain;
using MyApplication.API.Models.DTOs;
using MyApplication.API.Repositories.Interfaces;

namespace MyApplication.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RegionsController : ControllerBase
{
   private readonly IRegionRepository _regionRepository;
   private readonly IMapper _mapper;
   private readonly ILogger<RegionsController> _logger;

   public RegionsController(IRegionRepository regionRepository,
                            IMapper mapper,
                            ILogger<RegionsController> logger)
   {
      this._regionRepository = regionRepository;
      this._mapper = mapper;
      this._logger = logger;
   }

   // GET ALL REGIONS
   // GET: https://localhost:portnumber/api/regions
   [HttpGet]
   //[Authorize]
   public async Task<IActionResult> GetAll()
   {
      _logger.LogInformation("GetAll Regions called - STARTS");

      var regionsDomain = await _regionRepository.GetAllAsync();

      _logger.LogInformation("GetAll Regions called - FINISHES");

      // Return DTOs
      var dtos = _mapper.Map<List<RegionDto>>(regionsDomain);

      return Ok(dtos);
   }


   // GET SINGLE REGION (Get Region By ID)
   // GET: https://localhost:portnumber/api/regions/{id}
   [HttpGet]
   [Authorize(Roles = "Writer,Reader")]
   [Route("{id:Guid}")]
   public async Task<IActionResult> GetById([FromRoute] Guid id)
   {
      //var region = dbContext.Regions.Find(id);
      // Get Region Domain Model From Database
      var regionDomain = await _regionRepository.GetByIdAsync(id);

      if (regionDomain == null)
      {
         return NotFound();
      }

      var dto = _mapper.Map<RegionDto>(regionDomain);

      // Return DTO back to client
      return Ok(dto);
   }


   // POST To Create New Region
   // POST: https://localhost:portnumber/api/regions
   [HttpPost]
   [Authorize(Roles = "Writer")]
   [ValidateModel]
   public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
   {
      // Map or Convert DTO to Domain Model
      var regionDomainModel = _mapper.Map<Region>(addRegionRequestDto);

      // Use Domain Model to create Region
      regionDomainModel = await _regionRepository.CreateAsync(regionDomainModel);

      // Map Domain model back to DTO
      var regionDto = _mapper.Map<RegionDto>(regionDomainModel);

      return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
   }


   // Update region
   // PUT: https://localhost:portnumber/api/regions/{id}
   [HttpPut]
   [Authorize(Roles = "Writer")]
   [Route("{id:Guid}")]
   [ValidateModel]
   public async Task<IActionResult> Update([FromRoute] Guid id,
                                           [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
   {

      // Map DTO to Domain Model
      var regionDomainModel = _mapper.Map<Region>(updateRegionRequestDto);

      // Check if region exists
      regionDomainModel = await _regionRepository.UpdateAsync(id, regionDomainModel);

      if (regionDomainModel == null)
      {
         return NotFound();
      }

      var dto = _mapper.Map<RegionDto>(regionDomainModel);

      return Ok(dto);
   }


   // Delete Region
   // DELETE: https://localhost:portnumber/api/regions/{id}
   [HttpDelete]
   [Authorize(Roles = "Writer")]
   [Route("{id:Guid}")]
   public async Task<IActionResult> Delete([FromRoute] Guid id)
   {
      var regionDomainModel = await _regionRepository.DeleteAsync(id);

      if (regionDomainModel == null)
      {
         return NotFound();
      }

      var dto = _mapper.Map<RegionDto>(regionDomainModel);

      return Ok(dto);
   }

}
