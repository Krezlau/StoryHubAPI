using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StoryHubAPI.Models.DTOs
{
    public class CommentResponseDTO
    {
        public Guid Id { get; set; }

        public string Text { get; set; }

        public string Username { get; set; }

        public string UserId { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
