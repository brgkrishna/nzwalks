using System;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.Domain.DTO;

namespace NZWalks.API.Repository
{
	public class SqlRegionRepository : IRegionRepository
	{
		private readonly NZWalksDBContext _nZWalksDBContext;
		public SqlRegionRepository(NZWalksDBContext nZWalksDBContext)
		{
			_nZWalksDBContext = nZWalksDBContext;
		}
		public async Task<List<Region>> GetAllAsync(bool isAscending, string? filterOn, string? filterQuery,
			string? sortBy, int pageNumber, int pageSize)
		{
			var regions = _nZWalksDBContext.Regions.AsQueryable();

			if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
			{
				if (filterQuery.Equals("Code", StringComparison.OrdinalIgnoreCase))
					regions = regions.Where(x => x.Code == filterQuery);
				else if (filterQuery.Equals("Name", StringComparison.OrdinalIgnoreCase))
					regions = regions.Where(x => x.Name == filterQuery);
			}

			if(!string.IsNullOrWhiteSpace(sortBy))
			{
				if (sortBy.Equals("Code", StringComparison.OrdinalIgnoreCase))
					regions = isAscending ? regions.OrderBy(x => x.Code) : regions.OrderByDescending(x => x.Code);
				else if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
					regions = isAscending ? regions.OrderBy(x => x.Name) : regions.OrderByDescending(x => x.Name); 
			}

			var skipSize = (pageNumber - 1) * pageSize;
			regions = regions.Skip(skipSize).Take(pageSize);

			return await regions.ToListAsync();
		}

		public async Task<Region?> GetRegionById(Guid id)
		{
			return await _nZWalksDBContext.Regions.FindAsync(id);
		}

		public async Task<Region?> Create(Region region)
		{
			await _nZWalksDBContext.Regions.AddAsync(region);
			await _nZWalksDBContext.SaveChangesAsync();

			return region;
		}

		public async Task<Region?> Update(Guid id, UpdateRegionRequestDto regionRequestDto)
		{
			var regionDomainModel = await _nZWalksDBContext.Regions.FindAsync(id);

			if (regionDomainModel == null)
				return null;

			regionDomainModel.Code = regionRequestDto.Code;
			regionDomainModel.Name = regionRequestDto.Name;
			regionDomainModel.RegionImageUrl = regionRequestDto.RegionImageUrl;

			await _nZWalksDBContext.SaveChangesAsync();
			return regionDomainModel;
		}

		public async Task<Region?> Delete(Guid id)
		{
			var existingRegion = await _nZWalksDBContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

			if (existingRegion == null)
				return null;

			_nZWalksDBContext.Regions.Remove(existingRegion);
			await _nZWalksDBContext.SaveChangesAsync();

			return existingRegion;
		}
	}
}
