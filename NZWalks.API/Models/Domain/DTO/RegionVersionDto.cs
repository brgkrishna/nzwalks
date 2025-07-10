namespace NZWalks.API.Models.Domain.DTO
{
    public class RegionVersionDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string RegionName { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
