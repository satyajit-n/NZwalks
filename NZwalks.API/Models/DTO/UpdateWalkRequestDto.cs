using System.ComponentModel.DataAnnotations;

namespace NZwalks.API.Models.DTO
{
    public class UpdateWalkRequestDto
    {
        [Required]
        [MaxLength(50, ErrorMessage = "Maximum size of letter for name is 50")]
        public string Name { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Maximum size of letter for name is 100")]
        public string Description { get; set; }

        [Required]
        [Range(0, 50)]
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }

        [Required]
        public Guid DifficultyId { get; set; }

        [Required]
        public Guid RegionId { get; set; }
    }
}
