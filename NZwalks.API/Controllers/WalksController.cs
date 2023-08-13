using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZwalks.API.CustomActionFilters;
using NZwalks.API.Data;
using NZwalks.API.Models.Domain;
using NZwalks.API.Models.DTO;
using NZwalks.API.Repositories;

namespace NZwalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;

        public WalksController(NZWalksDbContext dbContext, IMapper mapper, IWalkRepository walkRepository)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }

        //Create Walks
        //POST method
        [HttpPost]
        [ValidateModelAttribute]
        public async Task<IActionResult> Create([FromBody] AddWalksRequestDto addWalksRequestDto)
        {
            //Mapping this AddWalksRequestDto to Walk Domain
            var walkDomainModel = mapper.Map<Walk>(addWalksRequestDto);

            walkDomainModel = await walkRepository.CreateAsync(walkDomainModel);

            return Ok(mapper.Map<WalkDto>(walkDomainModel));
        }

        //Get walks
        //GET METHOD : /api/walks?filterOn=Name&filterQuery=Track?sortBy=Name?isAscending=true?pageNumber=1?pageSize=10
        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] string? filterOn, 
            [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy,
            [FromQuery] bool? isAscending,
            [FromQuery] int pageNumber=1,
            [FromQuery] int pageSize=10)
        {
            var walkDomain = await walkRepository.GetAllAsync(filterOn,filterQuery,sortBy,isAscending ?? true,pageNumber,pageSize);

            //mapping walkdomain to walkDto and returning it
            return Ok(mapper.Map<List<WalkDto>>(walkDomain));
        }

        //Get Single walk
        //GETBYID method
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var walkDomain = await walkRepository.GetByIdAsync(id);

            if (walkDomain == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<WalkDto>(walkDomain));
        }

        //Update walk
        //UPDATE method
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModelAttribute]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateWalkRequestDto updateWalkRequestDto)
        {
            var walkDomain = mapper.Map<Walk>(updateWalkRequestDto);

            walkDomain = await walkRepository.UpdateAsync(id, walkDomain);

            if (walkDomain == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<WalkDto>(walkDomain));
        }

        //Delete walk
        //Delete Method
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var walkDomain = await walkRepository.DeleteAsync(id);

            if (walkDomain == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<WalkDto>(walkDomain));
        }
    }
}
