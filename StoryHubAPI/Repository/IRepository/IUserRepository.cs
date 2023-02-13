using StoryHubAPI.Models.DTOs;

namespace StoryHubAPI.Repository.IRepository
{
    public interface IUserRepository
    {
        Task<bool> IsUniqueUser(string username);
        Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);
        Task<UserDTO> Register(RegisterRequestDTO registerRequestDTO);
        Task<string> Refresh(string accessToken, string refreshToken);
        Task<bool> ChangePasswordAsync(string token, string newPassword);
    }
}
