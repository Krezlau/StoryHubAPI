using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using StoryHubAPI.Models;
using StoryHubAPI.Models.DTOs;
using StoryHubAPI.Repository.IRepository;
using StoryHubAPI.Services;
using System.ComponentModel.Design;
using System.Net;

namespace StoryHubAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoriesController : ControllerBase
    {
        private readonly IStoryRepository _storyRepo;
        private readonly IAccessTokenService _tokenService;
        private readonly IRepository<Tag> _tagRepo;

        public StoriesController(IStoryRepository storyRepo, IAccessTokenService tokenService, IRepository<Tag> tagRepo)
        {
            _storyRepo = storyRepo;
            _tokenService = tokenService;
            _tagRepo = tagRepo;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<APIResponse>> GetStories()
        {
            var currentUserId = _tokenService.RetrieveUserIdFromRequest(Request);

            if (currentUserId is null)
            {
                return BadRequest(new APIResponse()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    IsSuccess = false,
                    ErrorMessages = { "Couldn't read access token." }
                });
            }

            List<Story> stories = await _storyRepo.GetAllAsync(includeProperties: "Tags,Author");

            List<StoryResponseDTO> result = new List<StoryResponseDTO>(stories.Count);
            foreach (var story in stories)
            {
                result.Add(new StoryResponseDTO()
                {
                    Id = story.Id,
                    AuthorId = story.AuthorId,
                    AuthorName = story.Author.UserName,
                    CreatedAt = story.CreatedAt,
                    Text = story.Text,
                    Title = story.Title,
                    Tags = story.Tags.Select(t => t.Name).ToList(),
                    LikesCount = await _storyRepo.CountLikesAsync(story.Id),
                    CommentsCount = await _storyRepo.CountCommentsAsync(story.Id),
                    IfLikedByCurrentUser = await _storyRepo.IfLikedByCurrentUserAsync(story.Id, currentUserId)
                });
            }

            return Ok(new APIResponse()
            {
                StatusCode = HttpStatusCode.OK,
                IsSuccess = true,
                Result = result
            });
        }

        [HttpGet("{id}", Name = "GetStory")]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetStory(string id)
        {
            var currentUserId = _tokenService.RetrieveUserIdFromRequest(Request);

            if (currentUserId is null)
            {
                return BadRequest(new APIResponse()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    IsSuccess = false,
                    ErrorMessages = { "Couldn't read access token." }
                });
            }

            bool outcome = Guid.TryParseExact(id, "D", out Guid storyId);

            if (!outcome)
            {
                return NotFound(new APIResponse()
                {
                    StatusCode = HttpStatusCode.NotFound,
                    IsSuccess = false,
                    ErrorMessages = { "Invalid id." }
                });
            }

            Story? story = await _storyRepo.GetAsync(s => s.Id == storyId, includeProperties: "Tags,Author");

            if (story is null)
            {
                return NotFound(new APIResponse()
                {
                    StatusCode = HttpStatusCode.NotFound,
                    IsSuccess = false,
                    ErrorMessages = { "Couldn't find a story with specified id." }
                });
            }

            StoryResponseDTO response = new()
            {
                Id = story.Id,
                AuthorId = story.AuthorId,
                AuthorName = story.Author.UserName,
                CreatedAt = story.CreatedAt,
                Text = story.Text,
                Title = story.Title,
                Tags = story.Tags.Select(t => t.Name).ToList(),
                LikesCount = await _storyRepo.CountLikesAsync(story.Id),
                CommentsCount = await _storyRepo.CountCommentsAsync(story.Id),
                IfLikedByCurrentUser = await _storyRepo.IfLikedByCurrentUserAsync(story.Id, currentUserId)
            };

            return Ok(new APIResponse()
            {
                StatusCode = HttpStatusCode.OK,
                IsSuccess = true,
                Result = response
            });
        }

        [HttpGet("byUserId/{userId}")]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<APIResponse>> GetUserStories(string userId)
        {
            var currentUserId = _tokenService.RetrieveUserIdFromRequest(Request);

            if (currentUserId is null)
            {
                return BadRequest(new APIResponse()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    IsSuccess = false,
                    ErrorMessages = { "Couldn't read access token." }
                });
            }

            List<Story> stories = await _storyRepo.GetAllAsync(filter: s => s.AuthorId == userId ,includeProperties: "Tags,Author");

            List<StoryResponseDTO> result = new List<StoryResponseDTO>(stories.Count);
            foreach (var story in stories)
            {
                result.Add(new StoryResponseDTO()
                {
                    Id = story.Id,
                    AuthorId = story.AuthorId,
                    AuthorName = story.Author.UserName,
                    CreatedAt = story.CreatedAt,
                    Text = story.Text,
                    Title = story.Title,
                    Tags = story.Tags.Select(t => t.Name).ToList(),
                    LikesCount = await _storyRepo.CountLikesAsync(story.Id),
                    CommentsCount = await _storyRepo.CountCommentsAsync(story.Id),
                    IfLikedByCurrentUser = await _storyRepo.IfLikedByCurrentUserAsync(story.Id, currentUserId)
                });
            }

            return Ok(new APIResponse()
            {
                StatusCode = HttpStatusCode.OK,
                IsSuccess = true,
                Result = result
            });
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<APIResponse>> CreateStory([FromBody] StoryRequestDTO story)
        {
            var currentUserId = _tokenService.RetrieveUserIdFromRequest(Request);

            if (currentUserId is null)
            {
                return BadRequest(new APIResponse()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    IsSuccess = false,
                    ErrorMessages = { "Couldn't read access token." }
                });
            }

            List<Tag> tags = new(story.Tags.Count);
            List<string> errors = new List<string>();
            foreach (string tag in story.Tags)
            {
                Tag? newTag = await _tagRepo.GetAsync(t => t.Name == tag);
                if (newTag is null)
                {
                    errors.Add($"Invalid tag name [{tag}]");
                }
                else
                {
                    tags.Add(newTag);
                }
            }

            if (errors.Count > 0)
            {
                return BadRequest(new APIResponse()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    IsSuccess = false,
                    ErrorMessages = errors
                });
            }

            Story created = new()
            {
                AuthorId = currentUserId,
                Tags = tags,
                Text = story.Text,
                Title = story.Title
            };

            await _storyRepo.CreateAsync(created);

            return Created(created.Id.ToString(), new APIResponse()
            {
                StatusCode = HttpStatusCode.Created,
                IsSuccess = true,
                Result = created.Id.ToString()
            });
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> DeleteStory(string id)
        {
            var currentUserId = _tokenService.RetrieveUserIdFromRequest(Request);

            if (currentUserId is null)
            {
                return BadRequest(new APIResponse()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    IsSuccess = false,
                    ErrorMessages = { "Couldn't read access token." }
                });
            }

            bool outcome = Guid.TryParseExact(id, "D", out Guid storyId);

            if (!outcome)
            {
                return NotFound(new APIResponse()
                {
                    StatusCode = HttpStatusCode.NotFound,
                    IsSuccess = false,
                    ErrorMessages = { "Invalid id." }
                });
            }

            Story? storyToDelete = await _storyRepo.GetAsync(s => s.Id == storyId);

            if (storyToDelete is null)
            {
                return NotFound(new APIResponse()
                {
                    StatusCode = HttpStatusCode.NotFound,
                    IsSuccess = false,
                    ErrorMessages = { "Couldn't find a story with specified id." }
                });
            }
            if (storyToDelete.AuthorId != currentUserId)
            {
                return Forbid();
            }

            await _storyRepo.DeleteAsync(storyToDelete);

            return Ok(new APIResponse()
            {
                StatusCode = HttpStatusCode.OK,
                IsSuccess = true,
                Result = "Successfully deleted."
            });
        }
    }
}
