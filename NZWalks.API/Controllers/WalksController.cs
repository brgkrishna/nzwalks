using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.Domain.DTO;
using NZWalks.API.Repository;

namespace NZWalks.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class WalksController : ControllerBase
	{
		private readonly IWalksRepository _walksRepository;
		private readonly IMapper _mapper;

		public WalksController(IWalksRepository walksRepository, IMapper mapper)
		{
			_walksRepository = walksRepository;
			_mapper = mapper;
		}

		[HttpGet]
		// GET: /api/walks?filterOn=Name&filterQuery=Track&sortBy=Name&isAscending=true&pageeNumber=1&pageSize=10
		public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? query,
			[FromQuery] string? sortBy, [FromQuery] bool? isAscending, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 1000)
		{
			var walks = await _walksRepository.GetAll(isAscending ?? true, filterOn, query, sortBy, pageNumber, pageSize);
			return Ok(_mapper.Map<List<WalksDto>>(walks));
		}

		[HttpPost]
		[ValidateModel]
		public async Task<IActionResult> Create(AddWalkRequestDto addWalkRequestDto)
		{
			var walksDto = await _walksRepository.CreateWalk(addWalkRequestDto);

			return CreatedAtAction(nameof(Create), new { id = walksDto.Id }, walksDto);
		}

		[HttpGet]
		[Route("{id}")]
		public async Task<IActionResult> GetWalkById(Guid id)
		{
			var walksDto = await _walksRepository.GetWalkById(id);

			return
				walksDto == null
				?
				NotFound()
				:
				Ok(walksDto);
		}

		[HttpPut]
		[ValidateModel]
		public async Task<IActionResult> Update(Guid id, UpdateWalkRequestDto updateWalkRequestDto)
		{
			var walksDto = await _walksRepository.Update(id, updateWalkRequestDto);

			return
				walksDto == null
				?
				NotFound()
				:
				Ok(walksDto);
		}

		[HttpDelete]
		public async Task<IActionResult> Delete(Guid id)
		{
			var walksDto = await _walksRepository.Delete(id);

			return
				walksDto == null
				?
				NotFound()
				:
				Ok("Deleted Successfully");
		}
	}
}
