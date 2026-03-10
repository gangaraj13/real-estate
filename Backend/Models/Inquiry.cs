using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class Inquiry
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User? User { get; set; }

        public int PropertyId { get; set; }
        public Property? Property { get; set; }

        public int AgentId { get; set; }
        public Agent? Agent { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Message { get; set; } = string.Empty;

        public DateTime? PreferredDate { get; set; }

        // "Pending", "Responded", "Closed"
        [Required]
        [MaxLength(50)]
        public string Status { get; set; } = "Pending";

        public DateTime SentAt { get; set; } = DateTime.UtcNow;
    }
}