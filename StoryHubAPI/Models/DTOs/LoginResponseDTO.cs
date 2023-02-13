namespace StoryHubAPI.Models.DTOs
{
    public class LoginResponseDTO
    {
        public UserDTO User { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
