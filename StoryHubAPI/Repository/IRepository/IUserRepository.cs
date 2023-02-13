using StoryHubAPI.Models.DTOs;

namespace StoryHubAPI.Repository.IRepository
{
    public interface IUserRepository
    {
        Task<bool> IsUniqueUserAsync(string username);
        Task<LoginResponseDTO> LoginUserAsync(LoginRequestDTO loginRequestDTO);
        Task<UserDTO> RegisterUserAsync(RegisterRequestDTO registerRequestDTO);
        Task<string> RefreshAsync(string accessToken, string refreshToken);
        Task<bool> ChangePasswordAsync(string userId, string currentPassword, string newPassword);
    }
}
