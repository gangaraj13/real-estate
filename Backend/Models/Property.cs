namespace Backend.Models
{
    public class Properties
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Location { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public double AreaSqFt { get; set; }
        public bool IsAvailable { get; set; } = true;
        public DateTime ListedAt { get; set; } = DateTime.UtcNow;
        public int AgentId { get; set; }
        public Agent? Agent { get; set; }
    }
}