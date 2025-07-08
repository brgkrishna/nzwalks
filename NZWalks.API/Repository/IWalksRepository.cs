using NZWalks.API.Models.Domain;
using NZWalks.API.Models.Domain.DTO;

namespace NZWalks.API.Repository
{
	public interface IWalksRepository
	{
		Task<List<Walks>> GetAll(bool isAscending, string? filterOn = null, string? filterQuery = null, string? sortBy = null, int pageNumber = 1, int pageSize = 1000);
		Task<WalksDto> CreateWalk(AddWalkRequestDto addWalkRequestDto);
		Task<WalksDto?> GetWalkById(Guid id);
		Task<WalksDto?> Update(Guid id, UpdateWalkRequestDto updateWalkRequestDto);
		Task<WalksDto?> Delete(Guid id);
	}
}
