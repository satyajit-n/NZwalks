using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NZwalks.API.CustomActionFilters;
using NZwalks.API.Data;
using NZwalks.API.Models.Domain;
using NZwalks.API.Models.DTO;
using NZwalks.API.Repositories;
using System.Text.Json;

namespace NZwalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;
        private readonly ILogger<RegionsController> logger;

        public RegionsController(
            NZWalksDbContext dbContext,
            IRegionRepository regionRepository,
            IMapper mapper,
            ILogger<RegionsController> logger)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
            this.logger = logger;
        }


        //GET ALL REGION
        // GET: https://localhost:portnumber/api/region
        [HttpGet]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAll()
        {
            //throw new Exception("SOMETHING WENT WRONG");
            //Geting region from database
            var regionsDomains = await regionRepository.GetAllAsync();

            
            var demo = mapper.Map<List<RegionDto>>(regionsDomains);
            
            Console.WriteLine(demo);
            Console.WriteLine(regionsDomains);

            //maping domain models to DTO's then return DTO's
            return Ok(mapper.Map<List<RegionDto>>(regionsDomains));
        }

        // GET Region By id
        // GET: https://localhost:portnumber/api/region/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            //var region = dbContext.Regions.Find(id);
            var regionDomain = await regionRepository.GetByIdAsync(id);

            if (regionDomain == null)
            {
                return NotFound();
            }
            //Converting Domain to DTO's and returning it
            return Ok(mapper.Map<RegionDto>(regionDomain));
        }


        //POST : https://localhost:portnumber/api/region
        [HttpPost]
        [ValidateModelAttribute]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {

            // Convert DTO to domain model
            var regionDomainModel = mapper.Map<Region>(addRegionRequestDto);

            // Use domain model to create region
            regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);

            //converting region domain model to dto so that it can be sent to user
            var regionDto = mapper.Map<RegionDto>(regionDomainModel);

            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
        }

        // Update Region
        // UPDATE : https://localhost:portnumber/api/region/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            //mapping DTO to domain model
            var regionDomainModel = mapper.Map<Region>(updateRegionRequestDto);

            regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            //converting domain models to dto returning Dto user
            return Ok(mapper.Map<RegionDto>(regionDomainModel));

        }
        // Delete Region
        // DELETE : https://localhost:portnumber/api/region/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var regionDomainModel = await regionRepository.DeleteAsync(id);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            // maping domain to DTO return deleted object
            return Ok(mapper.Map<RegionDto>(regionDomainModel));
        }
    }
}
