using System.ComponentModel.DataAnnotations;

namespace NZWalksAPI.Models.DTO
{
    public class AddWalkDto
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Code has to be maximum of 100 characters")]
        public string Name { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Code has to be maximum of 100 characters")]
        public string Description { get; set; }
        [Required]
        [Range(0,100, ErrorMessage = "Code has to be maximum of 3 characters")]
        public double LengthInKm { get; set; }
        [Required]
        public string WalkImageUrl { get; set; }
        [Required]
        public Guid DifficultyId { get; set; }
        [Required]
        public Guid RegionId { get; set; }
    }
}
