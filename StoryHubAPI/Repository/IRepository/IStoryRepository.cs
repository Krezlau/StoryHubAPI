using StoryHubAPI.Models;

namespace StoryHubAPI.Repository.IRepository
{
    public interface IStoryRepository : IRepository<Story>
    {
        Task<int> CountCommentsAsync(Guid storyId);
        Task<int> CountLikesAsync(Guid storyId);
        Task<bool> IfLikedByCurrentUserAsync(Guid storyId, string userId);
    }
}
