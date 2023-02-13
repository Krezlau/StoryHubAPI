using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoryHubAPI.Models.DTOs;
using StoryHubAPI.Repository.IRepository;
using StoryHubAPI.Services;

namespace StoryHubAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IAccessTokenService _tokenService;

        public AuthController(IUserRepository userRepository, IAccessTokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO request)
        {
            var loginResponse = await _userRepository.Login(request);
            if (loginResponse.User is null)
            {
                return BadRequest();
            }
            return Ok(loginResponse);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO request)
        {
            bool ifUserNameUnique = await _userRepository.IsUniqueUser(request.Username);
            if (!ifUserNameUnique)
            {
                return BadRequest();
            }

            var user = await _userRepository.Register(request);
            if (user is null)
            {
                return BadRequest();
            }
            return Ok(user);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshRequestDTO request)
        {
            try
            {
                string token = await _userRepository.Refresh(request.AccessToken, request.RefreshToken);
                return Ok(token);
            } 
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("change-password")]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequestDTO request)
        {
            string? userId = _tokenService.RetrieveUserIdFromRequest(Request);

            if (userId is null)
            {
                return BadRequest();
            }

            bool outcome;
            try
            {
                outcome = await _userRepository.ChangePasswordAsync(userId, request.CurrentPassword, request.NewPassword);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            if (outcome)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
