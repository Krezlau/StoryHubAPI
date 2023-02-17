using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoryHubAPI.Models;
using StoryHubAPI.Models.DTOs;
using StoryHubAPI.Repository.IRepository;
using System.Net;
using System.Reflection.Metadata.Ecma335;

namespace StoryHubAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly IRepository<Tag> _tagsRepo;

        public TagsController(IRepository<Tag> tagsRepo)
        {
            _tagsRepo = tagsRepo;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<APIResponse<List<TagResponseDTO>>>> GetTags()
        {
            var tags = await _tagsRepo.GetAllAsync();

            var result = new List<TagResponseDTO>(tags.Count);
            foreach (Tag tag in tags)
            {
                result.Add(new TagResponseDTO()
                {
                    Id = tag.Id,
                    Name = tag.Name
                });
            }

            return Ok(new APIResponse<List<TagResponseDTO>>()
            {
                StatusCode = HttpStatusCode.OK,
                IsSuccess = true,
                Result = result
            });
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<APIResponse<string>>> CreateTag([FromBody] TagRequestDTO tagDTO)
        {
            Tag tag = new Tag() { Name = tagDTO.Name };

            try
            {
                await _tagsRepo.CreateAsync(tag);
            } catch (Exception)
            {
                return BadRequest(new APIResponse<string>()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    IsSuccess = false,
                    ErrorMessages = { "Something went wrong." }
                });
            }

            return Created(tag.Id.ToString(), new APIResponse<string>()
            {
                StatusCode = HttpStatusCode.Created,
                IsSuccess = true,
                Result = tag.Id.ToString()
            });
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<APIResponse<string>>> DeleteTag(string id)
        {
            bool outcome = Guid.TryParseExact(id, "D", out Guid guid);

            if (!outcome)
            {
                return BadRequest(new APIResponse<string>()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    IsSuccess = false,
                    ErrorMessages = { "Invalid id." }
                });
            }

            Tag? tag = await _tagsRepo.GetAsync(t => t.Id == guid);

            if (tag is null)
            {
                return BadRequest(new APIResponse<string>()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    IsSuccess = false,
                    ErrorMessages = { "Couldn't find a tag with specified id." }
                });
            }

            try
            {
                await _tagsRepo.DeleteAsync(tag);
                return Ok(new APIResponse<string>()
                {
                    StatusCode = HttpStatusCode.OK,
                    IsSuccess = true,
                    Result = "Successfully deleted."
                });
            }
            catch (Exception)
            {
                return BadRequest(new APIResponse<string>()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    IsSuccess = false,
                    ErrorMessages = { "Something went wrong. Couldn't delete the tag." }
                });
            }
        }
    }
}
