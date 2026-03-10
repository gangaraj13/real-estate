using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public class Property
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        // "Apartment", "Villa", "Plot", "Studio"
        [Required]
        [MaxLength(50)]
        public string Type { get; set; } = string.Empty;

        // "Available", "Sold", "Rented"
        [Required]
        [MaxLength(50)]
        public string Status { get; set; } = "Available";

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public double AreaSqFt { get; set; }

        [Required]
        [MaxLength(300)]
        public string Address { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string City { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Locality { get; set; } = string.Empty;

        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        // Stored as JSON string e.g. '["Pool","Gym","Parking"]'
        public string? AmenitiesJson { get; set; }

        public int ViewCount { get; set; } = 0;
        public DateTime ListedAt { get; set; } = DateTime.UtcNow;

        // Foreign key
        public int AgentId { get; set; }
        public Agent? Agent { get; set; }

        // Navigation properties
        public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
        public ICollection<Inquiry> Inquiries { get; set; } = new List<Inquiry>();
    }
}