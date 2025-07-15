using System.ComponentModel.DataAnnotations;

namespace NZWalks.UI.Models.Dto
{
	public class WalkViewModel
	{
		public string Name { get; set; }

		public string Description { get; set; }

		public double Length { get; set; }

		public string? WalkImageUrl { get; set; }

		public Guid DifficultyId { get; set; }
		
		public Guid RegionId { get; set; }
	}
}
