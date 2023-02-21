using Moq;
using StoryHubAPI.Controllers;
using StoryHubAPI.Models;
using StoryHubAPI.Repository.IRepository;
using StoryHubAPI.Services;
using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace StoryHubApiTests.Controllers
{
    public class StoriesControllerTests
    {
        [Fact]
        public async void GetStories_InvalidToken_ShouldReturnBadRequest() 
        {
            Mock<IStoryRepository> sRepoMock = new Mock<IStoryRepository>();
            Mock<IAccessTokenService> tokenServiceMock = new Mock<IAccessTokenService>();
            Mock<IRepository<Tag>> tRepoMock = new Mock<IRepository<Tag>>();

            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers.Add("Authentication", "Bearer aljsdfafdjldfas");

            var controller = new StoriesController(sRepoMock.Object, tokenServiceMock.Object, tRepoMock.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = httpContext
                }
            };

            var data = controller.GetStories().Result;
            var result = (BadRequestObjectResult) data.Result;
            var apiResponse = (APIResponse) result.Value;


            Assert.Equal(400, result.StatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, apiResponse.StatusCode);
            Assert.False(apiResponse.IsSuccess);
            Assert.Equal("Couldn't read access token.", apiResponse.ErrorMessages.First());
            Assert.Null(apiResponse.Result);
        }
        public void GetStories_ValidToken_ShouldReturnStoriesResponseWithOK() 
        {
            Mock<IStoryRepository> sRepoMock = new Mock<IStoryRepository>();
            Mock<IAccessTokenService> tokenServiceMock = new Mock<IAccessTokenService>();
            Mock<IRepository<Tag>> tRepoMock = new Mock<IRepository<Tag>>();

            var stories = new List<Story> { new Story()
            {
                Id = Guid.Parse("XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX"),
                AuthorId = "authorId",
                CreatedAt = DateTime.Parse("01-13-2022"),
                Text = "text",
                Title = "title",
                Tags = new List<Tag> { new Tag() { Name = "tag" } },
            } };

            sRepoMock.Setup(s => s.GetAllAsync(filter: It.IsAnyType ,includeProperties: It.IsAny<string>())).Returns(Task.FromResult());
            sRepoMock.Setup(s => s.CountLikesAsync(It.IsAny<Guid>())).Returns(Task.FromResult(1));
            sRepoMock.Setup(s => s.CountCommentsAsync(It.IsAny<Guid>())).Returns(Task.FromResult(2));
            sRepoMock.Setup(s => s.IfLikedByCurrentUserAsync(It.IsAny<Guid>(), It.IsAny<string>())).Returns(Task.FromResult(true));

            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers.Add("Authentication", "Bearer aljsdfafdjldfas");

            var controller = new StoriesController(sRepoMock.Object, tokenServiceMock.Object, tRepoMock.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = httpContext
                }
            };

            var data = controller.GetStories().Result;
            var result = (OkObjectResult) data.Result;
            var apiResponse = (APIResponse) result.Value;


            Assert.Equal(200, result.StatusCode);
            Assert.Equal(HttpStatusCode.OK, apiResponse.StatusCode);
            Assert.False(apiResponse.IsSuccess);
            Assert.Equal("Couldn't read access token.", apiResponse.ErrorMessages.First());
            Assert.Null(apiResponse.Result);
        }
        public void GetStory_InvalidToken_ShouldReturnBadRequest() { }
        public void GetStory_InvalidGuid_ShouldReturnNotFound() { }
        public void GetStory_StoryNotFound_ShouldReturnNotFound() { }
        public void GetStory_StoryFound_ShouldReturnOkWithStoryResponseDTO() { }
        public void GetUserStories_InvalidToken_ShouldReturnBadRequest() { }
        public void GetUserStories_UserIdIsNull_ShouldReturnBadRequest() { }
        public void GetUserStories_ValidToken_ShouldReturnOkWithStoriesResponse() { }
        public void CreateStory_InvalidStory_ShouldReturnBadRequest() { }
        public void CreateStory_InvalidToken_ShouldReturnBadRequest() { }
        public void CreateStory_OneInvalidTag_ShouldReturnBadRequest() { }
        public void CreateStory_MultipleInvalidTags_ShouldReturnBadRequest() { }
        public void CreateStory_ValidTags_ShouldReturnCreatedWithId() { }
        public void DeleteStory_InvalidToken_ShouldReturnBadRequest() { }
        public void DeleteStory_InvalidGuid_ShouldReturnNotFound() { }
        public void DeleteStory_StoryNotFound_ShouldReturnNotFound() { }
        public void DeleteStory_StoryDoesNotBelongToUser_ShouldReturnForbidden() { }
        public void DeleteStory_ValidRequest_ShouldReturnOk() { }
    }
}
