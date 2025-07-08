using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.Domain.DTO;

namespace NZWalks.API.Repository
{
	public class SqlWalksRepository : IWalksRepository
	{
		private readonly NZWalksDBContext _nZWalksDBContext;
		private readonly IMapper _mapper;

		public SqlWalksRepository(NZWalksDBContext nZWalksDBContext, IMapper mapper)
		{
			_nZWalksDBContext = nZWalksDBContext;
			_mapper = mapper;
		}

		public async Task<List<Walks>> GetAll(bool isAscending, string? filterOn, string? filterQuery, string? sortBy, int pageNumber = 1, int pageSize = 1000)
		{
			//var walks = await _nZWalksDBContext.Walks.Include("Difficulty").Include("Region").ToListAsync();

			var walks = _nZWalksDBContext.Walks.Include("Difficulty").Include("Region").AsQueryable();

			//Filter
			if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
			{
				if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
					walks = walks.Where(x => x.Name.Contains(filterQuery));
			}

			//sorting
			if (!string.IsNullOrWhiteSpace(sortBy))
			{
				if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
				{
					walks =
						isAscending == true
						?
						walks.OrderBy(x => x.Name)
						:
						walks.OrderByDescending(x => x.Name);
				}

				else if (sortBy.Equals("Length", StringComparison.OrdinalIgnoreCase))
				{
					walks =
						isAscending == true
						?
						walks.OrderBy(x => x.Length)
						:
						walks.OrderByDescending(x => x.Length);
				}
			}

			//Pagination

			var skipSize = (pageNumber - 1) * pageSize;
			walks = walks.Skip(skipSize).Take(pageSize);

			return await walks.ToListAsync();
		}

		public async Task<WalksDto> CreateWalk(AddWalkRequestDto addWalkRequestDto)
		{
			var walk = _mapper.Map<Walks>(addWalkRequestDto);
			await _nZWalksDBContext.Walks.AddAsync(walk);
			await _nZWalksDBContext.SaveChangesAsync();

			return _mapper.Map<WalksDto>(walk);
		}

		public async Task<WalksDto?> GetWalkById(Guid id)
		{
			var walkDomainModel = await _nZWalksDBContext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x => x.Id == id);

			if (walkDomainModel == null)
				return null;

			return _mapper.Map<WalksDto>(walkDomainModel);
		}

		public async Task<WalksDto?> Update(Guid id, UpdateWalkRequestDto updateWalkRequestDto)
		{
			var walkDomainModel = await _nZWalksDBContext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(w => w.Id == id);
			var difficulty = await _nZWalksDBContext.Difficulties.FindAsync(updateWalkRequestDto.DifficultyId);
			var region = await _nZWalksDBContext.Regions.FindAsync(updateWalkRequestDto.RegionId);


			if (walkDomainModel == null)
				return null;

			walkDomainModel.Description = updateWalkRequestDto.Description;
			walkDomainModel.Name = updateWalkRequestDto.Name;
			walkDomainModel.Length = updateWalkRequestDto.Length;

			if (difficulty != null)
				walkDomainModel.Difficulty = difficulty;

			if (region != null)
				walkDomainModel.Region = region;

			await _nZWalksDBContext.SaveChangesAsync();

			return _mapper.Map<WalksDto>(walkDomainModel);
		}

		public async Task<WalksDto?> Delete(Guid id)
		{
			var walkDomainModel = await _nZWalksDBContext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x => x.Id == id);

			if (walkDomainModel == null)
				return null;

			_nZWalksDBContext.Walks.Remove(walkDomainModel);
			await _nZWalksDBContext.SaveChangesAsync();

			return _mapper.Map<WalksDto>(walkDomainModel);
		}
	}
}
