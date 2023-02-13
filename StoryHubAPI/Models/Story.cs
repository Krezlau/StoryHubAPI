using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoryHubAPI.Models
{
    public class Story : AuditModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required]
        [MinLength(10)]
        public string Text { get; set; }

        [Required]
        [ForeignKey(nameof(AuthorId))]
        public string AuthorId { get; set; }

        public virtual User Author { get; set; }

        [MaxLength(5)]
        public virtual List<Tag> Tags { get; set; } = new List<Tag>();

        public virtual List<Comment> Comments { get; set; } = new List<Comment>();

        public virtual List<Like> Likes { get; set; } = new List<Like>();
    }
}
