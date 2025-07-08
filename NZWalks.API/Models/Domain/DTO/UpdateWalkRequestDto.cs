using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.Domain.DTO
{
	public class UpdateWalkRequestDto
	{
		[Required]
		public string Name { get; set; }

		[Required]
		[MaxLength(100, ErrorMessage = "Description cannot exceed 100 characters")]
		public string Description { get; set; }

		[Required]
		[Range(1,10000, ErrorMessage = "Length has to be in the range of 1 to 10000")]
		public double Length { get; set; }
		public string? WalkImageUrl { get; set; }

		[Required]
		public Guid DifficultyId { get; set; }
		[Required]
		public Guid RegionId { get; set; }
	}
}
