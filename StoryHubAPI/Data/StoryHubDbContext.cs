using Microsoft.EntityFrameworkCore;
using StoryHubAPI.Models;

namespace StoryHubAPI.Data
{
    public class StoryHubDbContext : DbContext
    {
        public DbSet<Story> Stories { get; set; }
        public DbSet<Tag> Tags { get; set; }
    }
}
