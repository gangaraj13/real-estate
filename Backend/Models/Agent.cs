using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class Agent
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(150)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string Phone { get; set; } = string.Empty;

        [Required]
        [MaxLength(150)]
        public string Agency { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Bio { get; set; }

        // Navigation properties
        public ICollection<Property> Properties { get; set; } = new List<Property>();
        public ICollection<Inquiry> Inquiries { get; set; } = new List<Inquiry>();
    }
}