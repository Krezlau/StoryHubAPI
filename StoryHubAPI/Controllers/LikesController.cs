using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoryHubAPI.Models;
using StoryHubAPI.Repository.IRepository;
using StoryHubAPI.Services;
using System.Net;

namespace StoryHubAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikesController : ControllerBase
    {
        private readonly IAccessTokenService _tokenService;
        private readonly IRepository<Like> _likeRepo;
        private readonly IStoryRepository _storyRepo;

        public LikesController(IAccessTokenService tokenService, IRepository<Like> likeRepo, IStoryRepository storyRepo)
        {
            _tokenService = tokenService;
            _likeRepo = likeRepo;
            _storyRepo = storyRepo;
        }

        [HttpPost("{storyId}")]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<APIResponse<string>>> LikeStory(string storyId)
        {
            var currentUserId = _tokenService.RetrieveUserIdFromRequest(Request);

            if (currentUserId is null)
            {
                return BadRequest(new APIResponse<string>()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    IsSuccess = false,
                    ErrorMessages = { "Couldn't read access token." }
                });
            }

            bool outcome = Guid.TryParseExact(storyId, "D", out Guid storyGuid);

            if (!outcome)
            {
                return BadRequest(new APIResponse<string>()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    IsSuccess = false,
                    ErrorMessages = { "Invalid id." }
                });
            }

            Story? story = await _storyRepo.GetAsync(s => s.Id == storyGuid);

            if (story is null || await _storyRepo.IfLikedByCurrentUserAsync(storyGuid, currentUserId))
            {
                return BadRequest(new APIResponse<string>()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    IsSuccess = false,
                    ErrorMessages = { story is null ? "Couldn't find a story with specified id." : "This story is already liked by the user." }
                });
            }

            Like like = new()
            {
                StoryId = storyGuid,
                UserId = currentUserId
            };

            try
            {
                await _likeRepo.CreateAsync(like);
                return Ok(new APIResponse<string>()
                {
                    StatusCode = HttpStatusCode.OK,
                    IsSuccess = true,
                    Result = "Successfully liked the story."
                });
            } catch (Exception)
            {
                return BadRequest(new APIResponse<string>()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    IsSuccess = false,
                    ErrorMessages = { "Something went wrong." }
                });
            }
        }

        [HttpDelete("{storyId}")]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<APIResponse<string>>> RemoveLike(string storyId)
        {
            var currentUserId = _tokenService.RetrieveUserIdFromRequest(Request);

            if (currentUserId is null)
            {
                return BadRequest(new APIResponse<string>()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    IsSuccess = false,
                    ErrorMessages = { "Couldn't read access token." }
                });
            }

            bool outcome = Guid.TryParseExact(storyId, "D", out Guid storyGuid);

            if (!outcome)
            {
                return BadRequest(new APIResponse<string>()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    IsSuccess = false,
                    ErrorMessages = { "Invalid id." }
                });
            }

            Story? story = await _storyRepo.GetAsync(s => s.Id == storyGuid);

            if (story is null)
            {
                return BadRequest(new APIResponse<string>()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    IsSuccess = false,
                    ErrorMessages = { "Couldn't find a story with specified id."}
                });
            }

            Like? like = await _likeRepo.GetAsync(l => l.StoryId == storyGuid && l.UserId == currentUserId);

            if (like is null)
            {
                return BadRequest(new APIResponse<string>()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    IsSuccess = false,
                    ErrorMessages = { "Story is not liked by the user." }
                });
            }

            try
            {
                await _likeRepo.DeleteAsync(like);
                return Ok(new APIResponse<string>()
                {
                    StatusCode = HttpStatusCode.OK,
                    IsSuccess = true,
                    Result = "Successfully unliked the story."
                });
            }
            catch (Exception)
            {
                return BadRequest(new APIResponse<string>()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    IsSuccess = false,
                    ErrorMessages = { "Something went wrong." }
                });
            }
        }
    }
}
