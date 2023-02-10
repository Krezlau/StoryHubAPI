using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoryHubAPI.Models
{
    public class Story
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
        public Guid AuthorId { get; set; }
        [Required]
        [MinLength(5)]
        [MaxLength(20)]
        public string AuthorName { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        [MinLength(1)]
        [MaxLength(5)]
        public List<Tag> Tags { get; set; }
    }
}
