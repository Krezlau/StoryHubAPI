using StoryHubAPI.Models;
using System.Runtime.CompilerServices;

namespace StoryHubAPI.Services
{
    public interface IRefreshTokenService
    {
        Task<string> RetrieveOrGenerateRefreshTokenAsync(User user);
        Task<bool> ValidateRefreshTokenAsync(User user, string refreshToken);
        Task RevokeRefreshTokenAsync(User user);
    }
}
