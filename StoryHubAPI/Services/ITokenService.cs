using StoryHubAPI.Models;

namespace StoryHubAPI.Services
{
    public interface ITokenService
    {
        Task<string> GenerateJwtTokenAsync(User user);
        Task<string> RetrieveOrGenerateRefreshTokenAsync(User user);
        Task<bool> ValidateRefreshTokenAsync(User user, string refreshToken);
        string ReadUserId(string jwtToken);
    }
}
