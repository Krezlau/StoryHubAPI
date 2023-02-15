using System.ComponentModel.DataAnnotations;

namespace StoryHubAPI.Models.DTOs
{
    public class CommentRequestDTO
    {
        [Required]
        [MinLength(1)]
        public string Text { get; set; }
    }
}
