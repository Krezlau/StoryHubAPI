using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoryHubAPI.Models;
using StoryHubAPI.Models.DTOs;
using StoryHubAPI.Repository.IRepository;
using StoryHubAPI.Services;
using System.Net;

namespace StoryHubAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly IAccessTokenService _tokenService;
        private readonly IRepository<Comment> _commentRepo;
        private readonly IStoryRepository _storyRepo;

        public CommentsController(IAccessTokenService tokenService, IRepository<Comment> commentRepo, IStoryRepository storyRepo)
        {
            _tokenService = tokenService;
            _commentRepo = commentRepo;
            _storyRepo = storyRepo;
        }

        [HttpGet("story/{storyId}")]
        public async Task<ActionResult<APIResponse<List<CommentResponseDTO>>>> GetComments(string storyId)
        {
            bool outcome = Guid.TryParseExact(storyId, "D", out Guid storyGuid);

            if (!outcome)
            {
                return BadRequest(new APIResponse<List<CommentResponseDTO>> ()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    IsSuccess = false,
                    ErrorMessages = { "Invalid id." }
                });
            }

            Story? story = await _storyRepo.GetAsync(s => s.Id == storyGuid);

            if (story is null)
            {
                return BadRequest(new APIResponse<List<CommentResponseDTO>> ()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    IsSuccess = false,
                    ErrorMessages = { "Couldn't find a story with specified id." }
                });
            }

            List<Comment> comments = await _commentRepo.GetAllAsync(c => c.StoryId == storyGuid, includeProperties: "User");
            List<CommentResponseDTO> result = new List<CommentResponseDTO>(comments.Count);   

            foreach (Comment comment in comments)
            {
                result.Add(new CommentResponseDTO()
                {
                    Id = comment.Id,
                    CreatedAt = comment.CreatedAt,
                    Text = comment.Text,
                    Username = comment.User is not null ? comment.User.UserName : "[deleted user]"
                });
            }

            return Ok(new APIResponse<List<CommentResponseDTO>>()
            {
                StatusCode = HttpStatusCode.OK,
                IsSuccess = true,
                Result = result
            });
        }

        [HttpPost("story/{storyId}")]
        public async Task<ActionResult<APIResponse<string>>> CreateComment(string storyId, [FromBody] CommentRequestDTO comment)
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
                    ErrorMessages = { "Couldn't find a story with specified id." }
                });
            }

            Comment created = new()
            {
                Text = comment.Text,
                StoryId = storyGuid,
                UserId = currentUserId
            };

            try
            {
                await _commentRepo.CreateAsync(created);
                return CreatedAtRoute(created.Id, new APIResponse<string>()
                {
                    StatusCode = HttpStatusCode.Created,
                    IsSuccess = true,
                    Result = "Successfully created."
                });
            } catch (Exception)
            {
                return BadRequest(new APIResponse<string>()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    IsSuccess = false,
                    ErrorMessages = { "Something went wrong" }
                });
            }
        }
    }
}
