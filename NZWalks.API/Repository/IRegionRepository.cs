using NZWalks.API.Models.Domain;
using NZWalks.API.Models.Domain.DTO;

namespace NZWalks.API.Repository
{
	public interface IRegionRepository
	{
		Task<List<Region>> GetAllAsync(bool isAscending, string? filterOn, string? filterQuery, string? sortBy, int pageNumber, int pageSize);
		Task<Region?> GetRegionById(Guid id);
		Task<Region?> Create(Region region);
		Task<Region?> Update(Guid guid, UpdateRegionRequestDto regionDomainModel);
		Task<Region?> Delete(Guid id);
	}
}
