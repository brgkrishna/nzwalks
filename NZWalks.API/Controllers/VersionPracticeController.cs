using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.Domain.DTO;

namespace NZWalks.API.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    public class VersionPracticeController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly NZWalksDBContext _nzDBContext;

        public VersionPracticeController(IMapper mapper, NZWalksDBContext nzDBContext)
        {
            this._mapper = mapper;
            this._nzDBContext = nzDBContext;
        }

        [MapToApiVersion("1.0")]
        [HttpGet]
        public async Task<IActionResult> GetV1()
        {
            var region = await _nzDBContext.Regions.ToListAsync();

            return Ok(_mapper.Map<List<RegionDto>>(region));
        }

        [MapToApiVersion("2.0")]
        [HttpGet]
        public async Task<IActionResult> GetV2()
        {
            var regions = await _nzDBContext.Regions.ToListAsync();

            var versionList = regions
                .Select((region) => new RegionVersionDto
                {
                    Id = region.Id, // Or use any logic to generate ID
                    RegionName = region.Name,
                    Code = region.Code,
                    RegionImageUrl = region.RegionImageUrl
                });

            return Ok(versionList);
        }
    }
}
