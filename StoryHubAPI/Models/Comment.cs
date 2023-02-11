using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoryHubAPI.Models
{
    public class Comment : AuditModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        [MinLength(1)]
        public string Text { get; set; }

        [Required]
        [ForeignKey(nameof(UserId))]
        public string UserId { get; set; }
        public virtual User User { get; set; }
        [Required]
        [ForeignKey(nameof(StoryId))]
        public Guid StoryId { get; set; }
        public virtual Story Story { get; set; }
    }
}
