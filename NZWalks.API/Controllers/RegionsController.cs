using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.Domain.DTO;
using NZWalks.API.Repository;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly IRegionRepository _regionRepository;
        private readonly IMapper _mapper;

        public RegionsController(NZWalksDBContext nzWalksDBContext, IRegionRepository regionRepository, IMapper mapper)
        {

            _regionRepository = regionRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAll(bool? isAscending, string? filterOn, string? filterQuery, string? sortBy, int pageNumber, int pageSize)
        {
            //Get data from Database --> Domain
            var regions = await _regionRepository.GetAllAsync(isAscending ?? true, filterOn, filterQuery, sortBy, pageNumber, pageSize);

            //Map the domain to DTOs
            //var regionsDTO = regions.Select(region => new
            //{
            //	region.Id,
            //	region.Code,
            //	region.Name,
            //	region.RegionImageUrl
            //}).ToList();

            var regionsDto = _mapper.Map<List<RegionDto>>(regions);

            //Return DTO's
            return Ok(regionsDto);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetRegionById(Guid id)
        {
            var region = await _regionRepository.GetRegionById(id);

            return
                region == null
                ?
                NotFound()
                :
                Ok(_mapper.Map<RegionDto>(region));
        }

        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create(AddRegionRequestDto addRegionRequestDto)
        {
            var regionDomainModel = _mapper.Map<Region>(addRegionRequestDto);

            await _regionRepository.Create(regionDomainModel);

            var regionDto = _mapper.Map<RegionDto>(regionDomainModel);

            return CreatedAtAction(nameof(GetRegionById), new { id = regionDto.Id }, regionDto);
        }

        [HttpPut]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update(Guid guid, [FromBody] UpdateRegionRequestDto addRegionRequestDto)
        {

            var regionDomainModel = await _regionRepository.Update(guid, addRegionRequestDto);

            if (regionDomainModel == null)
                return NotFound();

            var regionDto = _mapper.Map<RegionDto>(regionDomainModel);

            return Ok(regionDto);
        }

        [HttpDelete]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var regionDomainModel = await _regionRepository.Delete(id);

            if (regionDomainModel == null)
                return NotFound();

            return Ok("Deleted successfully");
        }
    }
}
