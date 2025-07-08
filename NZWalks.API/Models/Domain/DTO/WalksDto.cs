namespace NZWalks.API.Models.Domain.DTO
{
	public class WalksDto
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public double Length { get; set; }
		public string? WalkImageUrl { get; set; }

		//Navigation properties
		public DifficultyDto Difficulty { get; set; }
		public RegionDto Region { get; set; }
	}
}
