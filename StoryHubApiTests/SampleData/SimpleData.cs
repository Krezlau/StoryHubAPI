using StoryHubAPI.Models;
using StoryHubAPI.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryHubApiTests.SampleData
{
    public static class SimpleData
    {
        public static Story SampleStory = new Story()
        {
            Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
            AuthorId = "authorId",
            CreatedAt = DateTime.Parse("01-12-2022"),
            Text = "text",
            Title = "title",
            Tags = new List<Tag> { new Tag() { Name = "tag" } },
        };

        public static StoryRequestDTO SampleStoryRequest = new StoryRequestDTO()
        {
            Title = "title",
            Text = "text",
            Tags = new List<string> { "tag1", "tag2" }
        };

        public static Tag SampleTagOne = new Tag()
        {
            Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
            CreatedAt = DateTime.Parse("01-12-2022"),
            Name = "tag1",
        };

        public static Tag SampleTagTwo = new Tag()
        {
            Id = Guid.Parse("baaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
            CreatedAt = DateTime.Parse("01-12-2022"),
            Name = "tag2",
        };

        public static List<Story> SingleStoryList = new List<Story> { SampleStory };
    }
}
