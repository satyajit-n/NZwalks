using System.ComponentModel.DataAnnotations;

namespace NZwalks.API.Models.DTO
{
    public class AddRegionRequestDto
    {
        [Required]
        [MinLength(2, ErrorMessage = "Code has to be in at least 2 letter")]
        [MaxLength(3, ErrorMessage = "Code can not be greater than 3 letter")]
        public string Code { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Maximum size of letter for name is 50")]
        public string Name { get; set; }

        public string? RegionImageUrl { get; set; }
    }
}
