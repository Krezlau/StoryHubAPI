using System.ComponentModel.DataAnnotations;

namespace StoryHubAPI.Models.DTOs
{
    public class TagResponseDTO
    {
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        public Guid Id { get; set; }
    }
}
