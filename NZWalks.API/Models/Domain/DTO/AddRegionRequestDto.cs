using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.Domain.DTO
{
	public class AddRegionRequestDto
	{
		[Required]
		[MinLength(3, ErrorMessage = "Code has to be a minimum of 3 characters")]
		[MaxLength(3, ErrorMessage = "Code cannot exceed 3 characters")]
		public string Code { get; set; }

		[Required]
		[MaxLength(100, ErrorMessage = "Name cannot exceed 3 characters")]
		public string Name { get; set; }
		public string? RegionImageUrl { get; set; }
	}
}
