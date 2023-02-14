using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoryHubAPI.Exceptions;
using StoryHubAPI.Models;
using StoryHubAPI.Models.DTOs;
using StoryHubAPI.Repository.IRepository;
using StoryHubAPI.Services;
using System.Net;

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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse<LoginResponseDTO>>> Login([FromBody] LoginRequestDTO request)
        {
            LoginResponseDTO loginResponse;
            try
            {
                loginResponse = await _userRepository.LoginUserAsync(request);
            }
            catch (AuthException e)
            {
                return BadRequest(new APIResponse<LoginResponseDTO>()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    IsSuccess = false,
                    ErrorMessages = { e.Message }
                });
            }

            return Ok(new APIResponse<LoginResponseDTO>()
            {
                StatusCode = HttpStatusCode.OK,
                IsSuccess = true,
                Result = loginResponse
            });
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse<UserDTO>>> Register([FromBody] RegisterRequestDTO request)
        {
            bool ifUserNameUnique = await _userRepository.IsUniqueUserAsync(request.Username);
            if (!ifUserNameUnique)
            {
                return BadRequest(new APIResponse<UserDTO>()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    IsSuccess = false,
                    ErrorMessages = { "Username already taken." }
                });
            }

            try
            {
                var user = await _userRepository.RegisterUserAsync(request);
                return Ok(new APIResponse<UserDTO>() 
                { 
                    StatusCode = HttpStatusCode.OK,
                    IsSuccess = true,
                    Result = user
                });
            }
            catch (AuthException e)
            {
                return BadRequest(new APIResponse<UserDTO>() 
                { 
                    StatusCode = HttpStatusCode.BadRequest,
                    IsSuccess = false,
                    ErrorMessages = e.Message.Split("\r\n").ToList()
                });
            }

        }

        [HttpPost("refresh")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse<string>>> Refresh([FromBody] RefreshRequestDTO request)
        {
            try
            {
                string token = await _userRepository.RefreshAsync(request.AccessToken, request.RefreshToken);
                return Ok(new APIResponse<string>()
                { 
                    StatusCode = HttpStatusCode.OK,
                    IsSuccess = true,
                    Result = token
                });
            } 
            catch (AuthException e)
            {
                return BadRequest(new APIResponse<string>() 
                { 
                    StatusCode = HttpStatusCode.BadRequest,
                    IsSuccess = false,
                    ErrorMessages = { e.Message }
                });
            }
        }

        [HttpPost("change-password")]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<APIResponse<string>>> ChangePassword([FromBody] ChangePasswordRequestDTO request)
        {
            string? userId = _tokenService.RetrieveUserIdFromRequest(Request);

            if (userId is null)
            {
                return BadRequest(new APIResponse<string>()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    IsSuccess = false,
                    ErrorMessages = { "Invalid access token." }
                });
            }

            bool outcome;
            try
            {
                outcome = await _userRepository.ChangePasswordAsync(userId, request.CurrentPassword, request.NewPassword);
            }
            catch (AuthException e)
            {
                return BadRequest(new APIResponse<string>()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    IsSuccess = false,
                    ErrorMessages = e.Message.Split("\r\n").ToList()
                });
            }
            if (!outcome)
            {
                return BadRequest(new APIResponse<string>() 
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    IsSuccess = false,
                    ErrorMessages = { "Invalid access token" }
                });
            }
            return Ok(new APIResponse<string>()
            {
                StatusCode = HttpStatusCode.OK,
                IsSuccess = true,
                Result = "Successfully changed the password."
            });
        }
    }
}
