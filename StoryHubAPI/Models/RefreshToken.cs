using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoryHubAPI.Models
{
    public class RefreshToken
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Value { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        public bool IsRevoked { get; set; } = false;
        [Required]
        public bool IsActive { get; set; } = false;
        [Required]
        [ForeignKey(nameof(UserId))]
        public string UserId { get; set; }
        public virtual User User { get; set; }
    }
}
