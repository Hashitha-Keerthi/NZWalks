using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.CustomActionFilters;
using NZWalksAPI.Models.Domains;
using NZWalksAPI.Models.DTO;
using NZWalksAPI.Repositories;

namespace NZWalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;

        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }

        [HttpPost]
        [ValidateModel]

        public async Task<IActionResult> Create([FromBody] AddWalkDto addWalkDto)
        {
            var walkDomainModel = mapper.Map<Walk>(addWalkDto);
            await walkRepository.CreateAsync(walkDomainModel);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize =1000)
        {
         var walksDomainModel = await walkRepository.GetAllAsync(filterOn, filterQuery, sortBy, isAscending ?? true, pageNumber, pageSize);
         return Ok(walksDomainModel);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var walkDomainModel = await walkRepository.GetById(id);
            if(walkDomainModel == null)
            {
                return NotFound(); 
            }

            return Ok(mapper.Map<WalkDto>(walkDomainModel));
        }

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateById([FromRoute] Guid id, UpdateWalkDto updateWalkDto)
        {
            var walkDomainModel = mapper.Map<Walk>(updateWalkDto);
            var regionDomain = await walkRepository.UpdateByIdAsync(id, walkDomainModel);

            if (regionDomain == null)
            {
                return NotFound();
            }

            return Ok();
        }

        // Delete a walk by id
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteWalk([FromRoute] Guid id)
        {
            var walkDomain = await walkRepository.DeleteByIdAsync(id);
            if (walkDomain == null)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
