using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.ComponentModel.DataAnnotations;

namespace StoryHubAPI.Models
{
    public class User : IdentityUser
    {
        [Required]
        public DateTime CreatedAt { get; set; }

        public virtual List<Story> Stories { get; set; }

        public virtual List<Comment> Comments { get; set; }

        public virtual List<Like> Likes { get; set; }

        public virtual RefreshToken RefreshToken { get; set; }
    }
}
