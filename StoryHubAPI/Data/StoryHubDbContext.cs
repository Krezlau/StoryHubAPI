using Microsoft.EntityFrameworkCore;
using StoryHubAPI.Models;

namespace StoryHubAPI.Data
{
    public class StoryHubDbContext : DbContext
    {
        public StoryHubDbContext(DbContextOptions<StoryHubDbContext> options) : base(options) { }

        public DbSet<Story> Stories { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<Story>()
            //    .HasMany(s => s.Tags)
            //    .WithMany(t => t)
        }
    }
}
