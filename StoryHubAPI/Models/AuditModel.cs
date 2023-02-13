namespace StoryHubAPI.Models
{
    public abstract class AuditModel
    {
        public DateTime CreatedAt { get; set; }
        public DateTime? LastModifiedAt { get; set; }
    }
}
