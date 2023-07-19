using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalksAPI.CustomActionFilters;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domains;
using NZWalksAPI.Models.DTO;
using NZWalksAPI.Repositories;

namespace NZWalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NzWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(NzWalksDbContext dbContext, IRegionRepository regionRepository, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }
        //GET all regions from the DB
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
           var regions = await regionRepository.GetAllRegionAsync();

            return Ok(mapper.Map<List<RegionDto>>(regions));
        }

        //GET region by id from the DB
        [HttpGet]
        [Route("id:Guid")]
        public async Task<IActionResult> GetById([FromRoute] Guid id )
        {
            // var regionDomain = await dbContext.Regions.FirstOrDefaultAsync(x=>x.id == id);
            var regionDomain = await regionRepository.GetRegionById(id);

            var regionDto = new List<RegionDto>();

            if (regionDomain == null)
            {
                return NotFound();
            }
        
            return Ok(mapper.Map<RegionDto>(regionDomain));
        }

        // POST request for create a region
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddRegionDto addRegionDto)
        {
            var regionDomainModel = mapper.Map<Region>(addRegionDto);

            await regionRepository.Create(regionDomainModel);

            var regionDto = mapper.Map<RegionDto>(regionDomainModel);


            return CreatedAtAction(nameof(GetById), new { id = regionDto.id }, regionDto);

        }

        // Update Region
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateRegion([FromRoute] Guid id, [FromBody] UpdateRegionDto updateRegionDto)
        {
            // Check for the region first
            var UpdateRegionDomain = mapper.Map<Region>(updateRegionDto);
            var regionDomain = await regionRepository.Update(id, UpdateRegionDomain);
            //var regionDomain = await dbContext.Regions.FirstOrDefaultAsync(x => x.id == id);

            if (regionDomain == null)
            {
                return NotFound();
            }

            var regionDto = mapper.Map<RegionDto>(regionDomain);

            return Ok(regionDto);

        }

        // Delete a region by id
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteRegion([FromRoute] Guid id)
        {
            var regionDomain = await regionRepository.Delete(id);
            if (regionDomain == null)
            {
                return NotFound();
            }

            return Ok();
        }

    }
}
