using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StoryHubAPI.Models;

namespace StoryHubAPI.Data
{
    public class StoryHubDbContext : IdentityDbContext<User>
    {
        public StoryHubDbContext(DbContextOptions<StoryHubDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Story> Stories { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Like> Likes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>()
                .HasMany(u => u.Comments)
                .WithOne(c => c.User)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Likes)
                .WithOne(l => l.User)
                .OnDelete(DeleteBehavior.Restrict);
        }

        public override int SaveChanges()
        {
            var tracker = ChangeTracker;

            foreach (var entry in tracker.Entries())
            {
                if (entry.Entity is AuditModel)
                {
                    var referenceEntity = entry.Entity as AuditModel;
                    if (referenceEntity is not null)
                    {
                        switch (entry.State)
                        {
                            case EntityState.Added:
                                referenceEntity.CreatedAt = DateTime.Now;
                                break;
                            case EntityState.Modified:
                                referenceEntity.LastModifiedAt = DateTime.Now;
                                break;
                            default:
                                break;
                        }
                    }
                    
                }
            }

            return base.SaveChanges();
        }
    }
}
