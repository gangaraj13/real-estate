using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class Favorite
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User? User { get; set; }

        public int PropertyId { get; set; }
        public Property? Property { get; set; }

        // "Saved", "Shortlisted", "Visited", "Negotiating"
        [Required]
        [MaxLength(50)]
        public string Tag { get; set; } = "Saved";

        [MaxLength(500)]
        public string? Note { get; set; }

        public DateTime SavedAt { get; set; } = DateTime.UtcNow;
    }
}