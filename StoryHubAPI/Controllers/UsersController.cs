using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoryHubAPI.Models.DTOs;
using StoryHubAPI.Models;
using System.Net;
using StoryHubAPI.Repository.IRepository;

namespace StoryHubAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IRepository<User> _userRepo;

        public UsersController(IRepository<User> userRepo)
        {
            _userRepo = userRepo;
        }

        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<APIResponse>> GetTags(string id)
        {
            var user = await _userRepo.GetAsync(u => u.Id == id);

            if (user is null)
            {
                return NotFound(new APIResponse()
                {
                    StatusCode = HttpStatusCode.NotFound,
                    IsSuccess = false,
                    ErrorMessages = { "Could not find a user with specified id." }
                });
            }

            var resultUser = new UserDataDTO()
            {
                CreatedAt = user.CreatedAt,
                Email = user.Email,
                Username = user.UserName
            };

            return Ok(new APIResponse()
            {
                StatusCode = HttpStatusCode.OK,
                IsSuccess = true,
                Result = resultUser
            });
        }
    }
}
