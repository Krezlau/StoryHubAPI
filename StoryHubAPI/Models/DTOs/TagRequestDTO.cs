using System.ComponentModel.DataAnnotations;

namespace StoryHubAPI.Models.DTOs
{
    public class TagRequestDTO
    {
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
