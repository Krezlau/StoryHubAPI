using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StoryHubAPI.Models;

namespace StoryHubAPI.Data
{
    public class StoryHubDbContext : IdentityDbContext<User>
    {
        public StoryHubDbContext(DbContextOptions<StoryHubDbContext> options) : base(options) { }

        public override DbSet<User> Users { get; set; }
        public DbSet<Story> Stories { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

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

            modelBuilder.Entity<Tag>().HasData(new List<Tag>
            {
                new Tag { Name = "erotic", Id = Guid.NewGuid(), CreatedAt = DateTime.Now},
                new Tag { Name = "ghosts", Id = Guid.NewGuid(), CreatedAt = DateTime.Now},
                new Tag { Name = "horror", Id = Guid.NewGuid(), CreatedAt = DateTime.Now},
                new Tag { Name = "zombies", Id = Guid.NewGuid(), CreatedAt = DateTime.Now},
                new Tag { Name = "monsters", Id = Guid.NewGuid(), CreatedAt = DateTime.Now},
                new Tag { Name = "aliens", Id = Guid.NewGuid(), CreatedAt = DateTime.Now},
                new Tag { Name = "apocalypse", Id = Guid.NewGuid(), CreatedAt = DateTime.Now},
                new Tag { Name = "dystopia", Id = Guid.NewGuid(), CreatedAt = DateTime.Now},
                new Tag { Name = "utopia", Id = Guid.NewGuid(), CreatedAt = DateTime.Now},
                new Tag { Name = "worldbuilding", Id = Guid.NewGuid(), CreatedAt = DateTime.Now},
                new Tag { Name = "spiritual", Id = Guid.NewGuid(), CreatedAt = DateTime.Now},
                new Tag { Name = "science fiction", Id = Guid.NewGuid(), CreatedAt = DateTime.Now},
                new Tag { Name = "fantasy", Id = Guid.NewGuid(), CreatedAt = DateTime.Now},
                new Tag { Name = "fairy tale", Id = Guid.NewGuid(), CreatedAt = DateTime.Now},
                new Tag { Name = "mythology", Id = Guid.NewGuid(), CreatedAt = DateTime.Now},
                new Tag { Name = "historical fiction", Id = Guid.NewGuid(), CreatedAt = DateTime.Now},
                new Tag { Name = "historical", Id = Guid.NewGuid(), CreatedAt = DateTime.Now},
                new Tag { Name = "time travel", Id = Guid.NewGuid(), CreatedAt = DateTime.Now},
                new Tag { Name = "action", Id = Guid.NewGuid(), CreatedAt = DateTime.Now},
                new Tag { Name = "superhero", Id = Guid.NewGuid(), CreatedAt = DateTime.Now},
                new Tag { Name = "murder", Id = Guid.NewGuid(), CreatedAt = DateTime.Now},
                new Tag { Name = "thriller", Id = Guid.NewGuid(), CreatedAt = DateTime.Now},
                new Tag { Name = "humor", Id = Guid.NewGuid(), CreatedAt = DateTime.Now},
                new Tag { Name = "legend", Id = Guid.NewGuid(), CreatedAt = DateTime.Now},
                new Tag { Name = "young adult", Id = Guid.NewGuid(), CreatedAt = DateTime.Now},
                new Tag { Name = "futuristic", Id = Guid.NewGuid(), CreatedAt = DateTime.Now},
                new Tag { Name = "fiction", Id = Guid.NewGuid(), CreatedAt = DateTime.Now},
                new Tag { Name = "cyberpunk", Id = Guid.NewGuid(), CreatedAt = DateTime.Now},
                new Tag { Name = "dark fantasy", Id = Guid.NewGuid(), CreatedAt = DateTime.Now},
                new Tag { Name = "comedy", Id = Guid.NewGuid(), CreatedAt = DateTime.Now},
                new Tag { Name = "romance", Id = Guid.NewGuid(), CreatedAt = DateTime.Now}
            });
        }

        private void TrackChanges()
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
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            TrackChanges();

            return base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            TrackChanges();

            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            TrackChanges();

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override int SaveChanges()
        {
            TrackChanges();

            return base.SaveChanges();
        }
    }
}
