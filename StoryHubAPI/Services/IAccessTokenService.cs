using StoryHubAPI.Models;
using System.IdentityModel.Tokens.Jwt;

namespace StoryHubAPI.Services
{
    public interface IAccessTokenService
    {
        Task<string> GenerateJwtTokenAsync(User user);
        string ReadUserId(string jwtToken);
        string? RetrieveUserIdFromRequest(HttpRequest request);
    }
}
