using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace StoryHubAPI.Models
{
    public class User : IdentityUser
    {
        public virtual List<Story> Stories { get; set; }

        public virtual List<Comment> Comments { get; set; }

        public virtual List<Like> Likes { get; set; }
    }
}
