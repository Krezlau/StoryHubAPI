using System.ComponentModel.DataAnnotations;

namespace StoryHubAPI.Models.DTOs
{
    public class StoryRequestDTO
    {
        [Required]
        [MinLength(1)]
        [MaxLength(255)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MinLength(1)]
        public string Text { get; set; } = string.Empty;

        [Required]
        [MinLength(1)]
        [MaxLength(5)]
        public List<string> Tags { get; set; } = new List<string>();
    }
}
