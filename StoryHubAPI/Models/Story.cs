namespace StoryHubAPI.Models
{
    public class Story
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public Guid AuthorId { get; set; }
        public string AuthorName { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<Tag> Tags { get; set; }
    }
}
