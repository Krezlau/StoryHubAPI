using Microsoft.EntityFrameworkCore;
using StoryHubAPI.Data;
using StoryHubAPI.Models;
using StoryHubAPI.Repository.IRepository;

namespace StoryHubAPI.Repository
{
    public class StoryRepository : Repository<Story>, IStoryRepository
    {
        public StoryRepository(StoryHubDbContext context) : base(context)
        {
        }

        public async Task<int> CountCommentsAsync(Guid storyId)
        {
            return await _context.Comments.CountAsync(c => c.StoryId == storyId);
        }

        public async Task<int> CountLikesAsync(Guid storyId)
        {
            return await _context.Likes.CountAsync(l => l.StoryId == storyId);
        }

        public async Task<bool> IfLikedByCurrentUserAsync(Guid storyId, string userId)
        {
            return await _context.Likes.AnyAsync(l => l.UserId == userId && l.StoryId == storyId);
        }
    }
}
