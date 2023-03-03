using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StoryHubAPI.Models.DTOs
{
    public class StoryResponseDTO
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public string AuthorId { get; set; }

        public string AuthorName { get; set; }

        public DateTime CreatedAt { get; set; }

        public int LikesCount { get; set; }

        public int CommentsCount { get; set; }

        public List<string> Tags { get; set; }

        public bool IfLikedByCurrentUser { get; set; }
    }
}
