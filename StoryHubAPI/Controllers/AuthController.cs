using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoryHubAPI.Models.DTOs;
using StoryHubAPI.Repository.IRepository;

namespace StoryHubAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public AuthController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
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
    }
}
