using System.ComponentModel.DataAnnotations;

namespace StoryHubAPI.Models.DTOs
{
    public class StoryRequestDTO
    {
        [Required]
        [MinLength(1)]
        [MaxLength(255)]
        public string Title { get; set; }

        [Required]
        [MinLength(1)]
        public string Text { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(5)]
        public List<string> Tags { get; set; }
    }
}
