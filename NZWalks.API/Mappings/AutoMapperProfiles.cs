using AutoMapper;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.Domain.DTO;

namespace NZWalks.API.Mappings
{
	public class AutoMapperProfiles : Profile
	{
		public AutoMapperProfiles()
		{
			CreateMap<RegionDto, Region>().ReverseMap();
			CreateMap<DifficultyDto, Difficulty>().ReverseMap();
			CreateMap<AddRegionRequestDto, Region>().ReverseMap();
			CreateMap<UpdateRegionRequestDto, Region>().ReverseMap();
			CreateMap<AddWalkRequestDto, Walks>().ReverseMap();
			CreateMap<WalksDto, Walks>().ReverseMap();
			CreateMap<ImageUploadRequestDto, Image>().ReverseMap();
		}
	}
}
