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
using System.Linq.Expressions;
using StoryHubAPI.Models.DTOs;
using StoryHubApiTests.SampleData;

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

            var controller = new StoriesController(sRepoMock.Object, tokenServiceMock.Object, tRepoMock.Object);

            var data = controller.GetStories().Result;
            var result = (BadRequestObjectResult) data.Result;
            var apiResponse = (APIResponse) result.Value;

            Assert.Equal(400, result.StatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, apiResponse.StatusCode);
            Assert.False(apiResponse.IsSuccess);
            Assert.Equal("Couldn't read access token.", apiResponse.ErrorMessages.First());
            Assert.Null(apiResponse.Result);
        }

        [Fact]
        public async void GetStories_ValidToken_ShouldReturnStoriesResponseWithOK() 
        {
            Mock<IStoryRepository> sRepoMock = new Mock<IStoryRepository>();
            Mock<IAccessTokenService> tokenServiceMock = new Mock<IAccessTokenService>();
            Mock<IRepository<Tag>> tRepoMock = new Mock<IRepository<Tag>>();

            var stories = SimpleData.SingleStoryList;

            tokenServiceMock.Setup(t => t.RetrieveUserIdFromRequest(It.IsAny<HttpRequest>())).Returns("userId");
            sRepoMock.Setup(s => s.GetAllAsync(It.IsAny<Expression<Func<Story, bool>>>() ,It.IsAny<string>())).Returns(Task.FromResult(stories));
            sRepoMock.Setup(s => s.CountLikesAsync(It.IsAny<Guid>())).Returns(Task.FromResult(1));
            sRepoMock.Setup(s => s.CountCommentsAsync(It.IsAny<Guid>())).Returns(Task.FromResult(2));
            sRepoMock.Setup(s => s.IfLikedByCurrentUserAsync(It.IsAny<Guid>(), It.IsAny<string>())).Returns(Task.FromResult(true));

            var controller = new StoriesController(sRepoMock.Object, tokenServiceMock.Object, tRepoMock.Object);

            var data = controller.GetStories().Result;
            var result = (OkObjectResult) data.Result;
            var apiResponse = (APIResponse) result.Value;


            Assert.Equal(200, result.StatusCode);
            Assert.Equal(HttpStatusCode.OK, apiResponse.StatusCode);
            Assert.True(apiResponse.IsSuccess);
            Assert.Empty(apiResponse.ErrorMessages);

            var resultStoryList = (List<StoryResponseDTO>) apiResponse.Result;

            Assert.Single(resultStoryList);
            Assert.Equal(stories[0].Text, resultStoryList[0].Text);
            Assert.Equal(stories[0].AuthorId, resultStoryList[0].AuthorId);
            Assert.Equal(stories[0].Id, resultStoryList[0].Id);
            Assert.Equal(stories[0].CreatedAt, resultStoryList[0].CreatedAt);
            Assert.Equal(1, resultStoryList[0].LikesCount);
            Assert.Equal(2, resultStoryList[0].CommentsCount);
            Assert.True(resultStoryList[0].IfLikedByCurrentUser);
            Assert.Single(resultStoryList[0].Tags);
            Assert.Equal("tag", resultStoryList[0].Tags[0]);
        }

        [Fact]
        public void GetStory_InvalidToken_ShouldReturnBadRequest() 
        {
            Mock<IStoryRepository> sRepoMock = new Mock<IStoryRepository>();
            Mock<IAccessTokenService> tokenServiceMock = new Mock<IAccessTokenService>();
            Mock<IRepository<Tag>> tRepoMock = new Mock<IRepository<Tag>>();

            var controller = new StoriesController(sRepoMock.Object, tokenServiceMock.Object, tRepoMock.Object);

            var data = controller.GetStory("id").Result;
            var result = (BadRequestObjectResult) data.Result;
            var apiResponse = (APIResponse) result.Value;


            Assert.Equal(400, result.StatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, apiResponse.StatusCode);
            Assert.False(apiResponse.IsSuccess);
            Assert.Equal("Couldn't read access token.", apiResponse.ErrorMessages.First());
            Assert.Null(apiResponse.Result);
        }

        [Fact]
        public void GetStory_InvalidGuid_ShouldReturnNotFound() 
        {
            Mock<IStoryRepository> sRepoMock = new Mock<IStoryRepository>();
            Mock<IAccessTokenService> tokenServiceMock = new Mock<IAccessTokenService>();
            Mock<IRepository<Tag>> tRepoMock = new Mock<IRepository<Tag>>();

            tokenServiceMock.Setup(t => t.RetrieveUserIdFromRequest(It.IsAny<HttpRequest>())).Returns("userId");

            var controller = new StoriesController(sRepoMock.Object, tokenServiceMock.Object, tRepoMock.Object);

            var data = controller.GetStory("id").Result;
            var result = (NotFoundObjectResult)data.Result;
            var apiResponse = (APIResponse)result.Value;


            Assert.Equal(404, result.StatusCode);
            Assert.Equal(HttpStatusCode.NotFound, apiResponse.StatusCode);
            Assert.False(apiResponse.IsSuccess);
            Assert.Equal("Invalid id.", apiResponse.ErrorMessages.First());
            Assert.Null(apiResponse.Result);
        }

        [Fact]
        public void GetStory_StoryNotFound_ShouldReturnNotFound() 
        {
            Mock<IStoryRepository> sRepoMock = new Mock<IStoryRepository>();
            Mock<IAccessTokenService> tokenServiceMock = new Mock<IAccessTokenService>();
            Mock<IRepository<Tag>> tRepoMock = new Mock<IRepository<Tag>>();

            tokenServiceMock.Setup(t => t.RetrieveUserIdFromRequest(It.IsAny<HttpRequest>())).Returns("userId");

            var controller = new StoriesController(sRepoMock.Object, tokenServiceMock.Object, tRepoMock.Object);

            var data = controller.GetStory("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa").Result;
            var result = (NotFoundObjectResult)data.Result;
            var apiResponse = (APIResponse)result.Value;


            Assert.Equal(404, result.StatusCode);
            Assert.Equal(HttpStatusCode.NotFound, apiResponse.StatusCode);
            Assert.False(apiResponse.IsSuccess);
            Assert.Equal("Couldn't find a story with specified id.", apiResponse.ErrorMessages.First());
            Assert.Null(apiResponse.Result);
        }

        [Fact]
        public void GetStory_StoryFound_ShouldReturnOkWithStoryResponseDTO() 
        {
            Mock<IStoryRepository> sRepoMock = new Mock<IStoryRepository>();
            Mock<IAccessTokenService> tokenServiceMock = new Mock<IAccessTokenService>();
            Mock<IRepository<Tag>> tRepoMock = new Mock<IRepository<Tag>>();

            Story? story = SimpleData.SampleStory;

            tokenServiceMock.Setup(t => t.RetrieveUserIdFromRequest(It.IsAny<HttpRequest>())).Returns("userId");
            sRepoMock.Setup(s => s.GetAsync(It.IsAny<Expression<Func<Story, bool>>>(), It.IsAny<bool>(), It.IsAny<string>())).Returns(Task.FromResult(story));
            sRepoMock.Setup(s => s.CountLikesAsync(It.IsAny<Guid>())).Returns(Task.FromResult(1));
            sRepoMock.Setup(s => s.CountCommentsAsync(It.IsAny<Guid>())).Returns(Task.FromResult(2));
            sRepoMock.Setup(s => s.IfLikedByCurrentUserAsync(It.IsAny<Guid>(), It.IsAny<string>())).Returns(Task.FromResult(true));

            var controller = new StoriesController(sRepoMock.Object, tokenServiceMock.Object, tRepoMock.Object);

            var data = controller.GetStory("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa").Result;
            var result = (OkObjectResult)data.Result;
            var apiResponse = (APIResponse)result.Value;


            Assert.Equal(200, result.StatusCode);
            Assert.Equal(HttpStatusCode.OK, apiResponse.StatusCode);
            Assert.True(apiResponse.IsSuccess);
            Assert.Empty(apiResponse.ErrorMessages);

            var resultStory = (StoryResponseDTO) apiResponse.Result;

            Assert.Equal(story.Text, resultStory.Text);
            Assert.Equal(story.AuthorId, resultStory.AuthorId);
            Assert.Equal(story.Id, resultStory.Id);
            Assert.Equal(story.CreatedAt, resultStory.CreatedAt);
            Assert.Equal(1, resultStory.LikesCount);
            Assert.Equal(2, resultStory.CommentsCount);
            Assert.True(resultStory.IfLikedByCurrentUser);
            Assert.Single(resultStory.Tags);
            Assert.Equal("tag", resultStory.Tags[0]);
        }

        [Fact]
        public void GetUserStories_InvalidToken_ShouldReturnBadRequest()
        {
            Mock<IStoryRepository> sRepoMock = new Mock<IStoryRepository>();
            Mock<IAccessTokenService> tokenServiceMock = new Mock<IAccessTokenService>();
            Mock<IRepository<Tag>> tRepoMock = new Mock<IRepository<Tag>>();

            var controller = new StoriesController(sRepoMock.Object, tokenServiceMock.Object, tRepoMock.Object);

            var data = controller.GetUserStories("id").Result;
            var result = (BadRequestObjectResult)data.Result;
            var apiResponse = (APIResponse)result.Value;


            Assert.Equal(400, result.StatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, apiResponse.StatusCode);
            Assert.False(apiResponse.IsSuccess);
            Assert.Equal("Couldn't read access token.", apiResponse.ErrorMessages.First());
            Assert.Null(apiResponse.Result);
        }

        [Fact]
        public void GetUserStories_ValidToken_ShouldReturnOkWithStoriesResponse()
        {
            Mock<IStoryRepository> sRepoMock = new Mock<IStoryRepository>();
            Mock<IAccessTokenService> tokenServiceMock = new Mock<IAccessTokenService>();
            Mock<IRepository<Tag>> tRepoMock = new Mock<IRepository<Tag>>();

            var stories = SimpleData.SingleStoryList;

            tokenServiceMock.Setup(t => t.RetrieveUserIdFromRequest(It.IsAny<HttpRequest>())).Returns("userId");
            sRepoMock.Setup(s => s.GetAllAsync(It.IsAny<Expression<Func<Story, bool>>>(), It.IsAny<string>())).Returns(Task.FromResult(stories));
            sRepoMock.Setup(s => s.CountLikesAsync(It.IsAny<Guid>())).Returns(Task.FromResult(1));
            sRepoMock.Setup(s => s.CountCommentsAsync(It.IsAny<Guid>())).Returns(Task.FromResult(2));
            sRepoMock.Setup(s => s.IfLikedByCurrentUserAsync(It.IsAny<Guid>(), It.IsAny<string>())).Returns(Task.FromResult(true));

            var controller = new StoriesController(sRepoMock.Object, tokenServiceMock.Object, tRepoMock.Object);

            var data = controller.GetUserStories("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa").Result;
            var result = (OkObjectResult)data.Result;
            var apiResponse = (APIResponse)result.Value;

            Assert.Equal(200, result.StatusCode);
            Assert.Equal(HttpStatusCode.OK, apiResponse.StatusCode);
            Assert.True(apiResponse.IsSuccess);
            Assert.Empty(apiResponse.ErrorMessages);

            var resultStoryList = (List<StoryResponseDTO>)apiResponse.Result;

            Assert.Single(resultStoryList);
            Assert.Equal(stories[0].Text, resultStoryList[0].Text);
            Assert.Equal(stories[0].AuthorId, resultStoryList[0].AuthorId);
            Assert.Equal(stories[0].Id, resultStoryList[0].Id);
            Assert.Equal(stories[0].CreatedAt, resultStoryList[0].CreatedAt);
            Assert.Equal(1, resultStoryList[0].LikesCount);
            Assert.Equal(2, resultStoryList[0].CommentsCount);
            Assert.True(resultStoryList[0].IfLikedByCurrentUser);
            Assert.Single(resultStoryList[0].Tags);
            Assert.Equal("tag", resultStoryList[0].Tags[0]);
        }

        [Theory]
        public void CreateStory_InvalidStory_ShouldReturnBadRequest(Story story)
        {
            Mock<IStoryRepository> sRepoMock = new Mock<IStoryRepository>();
            Mock<IAccessTokenService> tokenServiceMock = new Mock<IAccessTokenService>();
            Mock<IRepository<Tag>> tRepoMock = new Mock<IRepository<Tag>>();

            tokenServiceMock.Setup(t => t.RetrieveUserIdFromRequest(It.IsAny<HttpRequest>())).Returns("userId");

            var controller = new StoriesController(sRepoMock.Object, tokenServiceMock.Object, tRepoMock.Object);

            var data = controller.CreateStory(story).Result;
            var result = (BadRequestObjectResult)data.Result;
            var apiResponse = (APIResponse)result.Value;

            Assert.Equal(400, result.StatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, apiResponse.StatusCode);
            Assert.False(apiResponse.IsSuccess);
            Assert.Null(apiResponse.Result);
        }

        [Fact]
        public void CreateStory_InvalidToken_ShouldReturnBadRequest()
        {
            Mock<IStoryRepository> sRepoMock = new Mock<IStoryRepository>();
            Mock<IAccessTokenService> tokenServiceMock = new Mock<IAccessTokenService>();
            Mock<IRepository<Tag>> tRepoMock = new Mock<IRepository<Tag>>();

            var controller = new StoriesController(sRepoMock.Object, tokenServiceMock.Object, tRepoMock.Object);

            var data = controller.CreateStory(SimpleData.SampleStoryRequest).Result;
            var result = (BadRequestObjectResult)data.Result;
            var apiResponse = (APIResponse)result.Value;

            Assert.Equal(400, result.StatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, apiResponse.StatusCode);
            Assert.False(apiResponse.IsSuccess);
            Assert.Equal("Couldn't read access token.", apiResponse.ErrorMessages.First());
            Assert.Null(apiResponse.Result);
        }

        [Fact]
        public void CreateStory_OneInvalidTag_ShouldReturnBadRequest()
        {
            Mock<IStoryRepository> sRepoMock = new Mock<IStoryRepository>();
            Mock<IAccessTokenService> tokenServiceMock = new Mock<IAccessTokenService>();
            Mock<IRepository<Tag>> tRepoMock = new Mock<IRepository<Tag>>();

            tokenServiceMock.Setup(t => t.RetrieveUserIdFromRequest(It.IsAny<HttpRequest>())).Returns("userId");
            tRepoMock.Setup(t => t.GetAsync(x => x.Name == "tag1", It.IsAny<bool>(), It.IsAny<string>()))
                .Returns(Task.FromResult(SimpleData.SampleTagOne));

            var controller = new StoriesController(sRepoMock.Object, tokenServiceMock.Object, tRepoMock.Object);

            var data = controller.CreateStory(SimpleData.SampleStoryRequest).Result;
            var result = (BadRequestObjectResult)data.Result;
            var apiResponse = (APIResponse)result.Value;

            Assert.Equal(400, result.StatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, apiResponse.StatusCode);
            Assert.False(apiResponse.IsSuccess);
            Assert.Equal($"Invalid tag name [{SimpleData.SampleTagTwo.Name}]", apiResponse.ErrorMessages.First());
            Assert.Null(apiResponse.Result);
        }

        [Fact]
        public void CreateStory_MultipleInvalidTags_ShouldReturnBadRequest()
        {
            Mock<IStoryRepository> sRepoMock = new Mock<IStoryRepository>();
            Mock<IAccessTokenService> tokenServiceMock = new Mock<IAccessTokenService>();
            Mock<IRepository<Tag>> tRepoMock = new Mock<IRepository<Tag>>();

            tokenServiceMock.Setup(t => t.RetrieveUserIdFromRequest(It.IsAny<HttpRequest>())).Returns("userId");

            var controller = new StoriesController(sRepoMock.Object, tokenServiceMock.Object, tRepoMock.Object);

            var data = controller.CreateStory(SimpleData.SampleStoryRequest).Result;
            var result = (BadRequestObjectResult)data.Result;
            var apiResponse = (APIResponse)result.Value;

            Assert.Equal(400, result.StatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, apiResponse.StatusCode);
            Assert.False(apiResponse.IsSuccess);
            Assert.Equal($"Invalid tag name [{SimpleData.SampleTagOne.Name}]", apiResponse.ErrorMessages[0]);
            Assert.Equal($"Invalid tag name [{SimpleData.SampleTagTwo.Name}]", apiResponse.ErrorMessages[1]);
            Assert.Null(apiResponse.Result);
        }

        [Fact]
        public void CreateStory_ValidTags_ShouldReturnCreatedWithId()
        {
            Mock<IStoryRepository> sRepoMock = new Mock<IStoryRepository>();
            Mock<IAccessTokenService> tokenServiceMock = new Mock<IAccessTokenService>();
            Mock<IRepository<Tag>> tRepoMock = new Mock<IRepository<Tag>>();

            tokenServiceMock.Setup(t => t.RetrieveUserIdFromRequest(It.IsAny<HttpRequest>())).Returns("userId");
            tRepoMock.Setup(t => t.GetAsync(It.IsAny<Expression<Func<Tag, bool>>>(), It.IsAny<bool>(), It.IsAny<string>()))
                .Returns(Task.FromResult(SimpleData.SampleTagOne));

            Story storyPassedToDb = null;
            sRepoMock.Setup(s => s.CreateAsync(It.IsAny<Story>()))
                .Callback<Story>(s =>
                {
                    s.Id = new Guid();
                    storyPassedToDb = s;
                });

            var controller = new StoriesController(sRepoMock.Object, tokenServiceMock.Object, tRepoMock.Object);

            StoryRequestDTO storyRequest = SimpleData.SampleStoryRequest;
            var data = controller.CreateStory(storyRequest).Result;
            var result = (CreatedResult)data.Result;
            var apiResponse = (APIResponse)result.Value;

            Assert.Equal(201, result.StatusCode);
            Assert.Equal(HttpStatusCode.Created, apiResponse.StatusCode);
            Assert.True(apiResponse.IsSuccess);
            Assert.Empty(apiResponse.ErrorMessages);

            Assert.NotNull(storyPassedToDb);
            Assert.Equal(storyRequest.Title, storyPassedToDb.Title);
            Assert.Equal(storyRequest.Text, storyPassedToDb.Text);
            Assert.Equal("userId", storyPassedToDb.AuthorId);
            Assert.Equal(result.Location, storyPassedToDb.Id.ToString());
        }

        [Fact]
        public void DeleteStory_InvalidToken_ShouldReturnBadRequest()
        {
            Mock<IStoryRepository> sRepoMock = new Mock<IStoryRepository>();
            Mock<IAccessTokenService> tokenServiceMock = new Mock<IAccessTokenService>();
            Mock<IRepository<Tag>> tRepoMock = new Mock<IRepository<Tag>>();

            var controller = new StoriesController(sRepoMock.Object, tokenServiceMock.Object, tRepoMock.Object);

            var data = controller.DeleteStory("id").Result;
            var result = (BadRequestObjectResult)data.Result;
            var apiResponse = (APIResponse)result.Value;

            Assert.Equal(400, result.StatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, apiResponse.StatusCode);
            Assert.False(apiResponse.IsSuccess);
            Assert.Equal("Couldn't read access token.", apiResponse.ErrorMessages.First());
            Assert.Null(apiResponse.Result);
        }

        [Fact]
        public void DeleteStory_InvalidGuid_ShouldReturnNotFound()
        {
            Mock<IStoryRepository> sRepoMock = new Mock<IStoryRepository>();
            Mock<IAccessTokenService> tokenServiceMock = new Mock<IAccessTokenService>();
            Mock<IRepository<Tag>> tRepoMock = new Mock<IRepository<Tag>>();

            tokenServiceMock.Setup(t => t.RetrieveUserIdFromRequest(It.IsAny<HttpRequest>())).Returns("userId");

            var controller = new StoriesController(sRepoMock.Object, tokenServiceMock.Object, tRepoMock.Object);

            var data = controller.DeleteStory("id").Result;
            var result = (NotFoundObjectResult)data.Result;
            var apiResponse = (APIResponse)result.Value;


            Assert.Equal(404, result.StatusCode);
            Assert.Equal(HttpStatusCode.NotFound, apiResponse.StatusCode);
            Assert.False(apiResponse.IsSuccess);
            Assert.Equal("Invalid id.", apiResponse.ErrorMessages.First());
            Assert.Null(apiResponse.Result);
        }

        [Fact]
        public void DeleteStory_StoryNotFound_ShouldReturnNotFound()
        {
            Mock<IStoryRepository> sRepoMock = new Mock<IStoryRepository>();
            Mock<IAccessTokenService> tokenServiceMock = new Mock<IAccessTokenService>();
            Mock<IRepository<Tag>> tRepoMock = new Mock<IRepository<Tag>>();

            tokenServiceMock.Setup(t => t.RetrieveUserIdFromRequest(It.IsAny<HttpRequest>())).Returns("userId");

            var controller = new StoriesController(sRepoMock.Object, tokenServiceMock.Object, tRepoMock.Object);

            var data = controller.DeleteStory("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa").Result;
            var result = (NotFoundObjectResult)data.Result;
            var apiResponse = (APIResponse)result.Value;


            Assert.Equal(404, result.StatusCode);
            Assert.Equal(HttpStatusCode.NotFound, apiResponse.StatusCode);
            Assert.False(apiResponse.IsSuccess);
            Assert.Equal("Couldn't find a story with specified id.", apiResponse.ErrorMessages.First());
            Assert.Null(apiResponse.Result);
        }

        [Fact]
        public void DeleteStory_StoryDoesNotBelongToUser_ShouldReturnForbidden()
        {
            Mock<IStoryRepository> sRepoMock = new Mock<IStoryRepository>();
            Mock<IAccessTokenService> tokenServiceMock = new Mock<IAccessTokenService>();
            Mock<IRepository<Tag>> tRepoMock = new Mock<IRepository<Tag>>();

            Story? story = SimpleData.SampleStory;

            tokenServiceMock.Setup(t => t.RetrieveUserIdFromRequest(It.IsAny<HttpRequest>())).Returns("userId");
            sRepoMock.Setup(s => s.GetAsync(It.IsAny<Expression<Func<Story, bool>>>(), It.IsAny<bool>(), It.IsAny<string>())).Returns(Task.FromResult(story));

            var controller = new StoriesController(sRepoMock.Object, tokenServiceMock.Object, tRepoMock.Object);

            var data = controller.DeleteStory("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa").Result;
            var result = (ForbidResult)data.Result;

            Assert.IsType<ForbidResult>(result);
        }

        [Fact]
        public void DeleteStory_ValidRequest_ShouldReturnOk()
        {
            Mock<IStoryRepository> sRepoMock = new Mock<IStoryRepository>();
            Mock<IAccessTokenService> tokenServiceMock = new Mock<IAccessTokenService>();
            Mock<IRepository<Tag>> tRepoMock = new Mock<IRepository<Tag>>();

            Story? story = SimpleData.SampleStory;
            Story storyPassed = null;

            tokenServiceMock.Setup(t => t.RetrieveUserIdFromRequest(It.IsAny<HttpRequest>())).Returns("authorId");
            sRepoMock.Setup(s => s.GetAsync(It.IsAny<Expression<Func<Story, bool>>>(), It.IsAny<bool>(), It.IsAny<string>())).Returns(Task.FromResult(story));
            sRepoMock.Setup(s => s.DeleteAsync(It.IsAny<Story>()))
                .Callback<Story>(st => storyPassed = st);

            var controller = new StoriesController(sRepoMock.Object, tokenServiceMock.Object, tRepoMock.Object);

            var data = controller.DeleteStory("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa").Result;
            var result = (OkObjectResult)data.Result;
            var apiResponse = (APIResponse)result.Value;


            Assert.Equal(200, result.StatusCode);
            Assert.Equal(HttpStatusCode.OK, apiResponse.StatusCode);
            Assert.True(apiResponse.IsSuccess);
            Assert.Empty(apiResponse.ErrorMessages);

            Assert.NotNull(storyPassed);
            Assert.Equal(story.Id, storyPassed.Id);
        }
    }
}
