using Microsoft.EntityFrameworkCore.Metadata;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoryHubAPI.Models
{
    public class Like : AuditModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

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
