using NZWalksAPI.Models.Domains;

namespace NZWalksAPI.Models.DTO
{
    public class WalkDto
    {
        public Guid id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInKm { get; set; }
        public double WalkImageUrl { get; set; }

        public RegionDto Region { get; set; }
        public DifficultyDto Difficulty { get; set; }
    }
}
