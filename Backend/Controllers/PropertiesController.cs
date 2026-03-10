using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Data;
using Backend.Models;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PropertiesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PropertiesController(AppDbContext context)
        {
            _context = context;
        }

        // GET /api/properties?minPrice=100000&maxPrice=500000&location=Austin&type=House&sortBy=price
        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] decimal? minPrice,
            [FromQuery] decimal? maxPrice,
            [FromQuery] string? location,
            [FromQuery] string? type,
            [FromQuery] string? sortBy)
        {
            var query = _context.Properties
                .Include(p => p.Agent)
                .AsQueryable();

            if (minPrice.HasValue)
                query = query.Where(p => p.Price >= minPrice.Value);

            if (maxPrice.HasValue)
                query = query.Where(p => p.Price <= maxPrice.Value);

            if (!string.IsNullOrWhiteSpace(location))
                query = query.Where(p => p.Location.Contains(location));

            if (!string.IsNullOrWhiteSpace(type))
                query = query.Where(p => p.Type.ToLower() == type.ToLower());

            // --- Sorting ---
            query = sortBy?.ToLower() switch
            {
                "price"     => query.OrderBy(p => p.Price),
                "price_desc"=> query.OrderByDescending(p => p.Price),
                "newest"    => query.OrderByDescending(p => p.ListedAt),
                _           => query.OrderBy(p => p.Id)
            };

            var results = await query.Select(p => new
            {
                p.Id,
                p.Title,
                p.Price,
                p.Location,
                p.Type,
                p.Bedrooms,
                p.Bathrooms,
                p.SquareFeet,
                p.IsAvailable,
                p.ListedAt,
                Agent = new { p.Agent!.Name, p.Agent.Email, p.Agent.Phone }
            }).ToListAsync();  

            return Ok(results);
        }

        // GET /api/properties/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var property = await _context.Properties
                .Include(p => p.Agent)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (property is null)
                return NotFound();

            return Ok(property);
        }

        // POST /api/properties
        [HttpPost]
        public async Task<IActionResult> Create(Property property)
        {
            _context.Properties.Add(property);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = property.Id }, property);
        }

        // PUT /api/properties/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Property updated)
        {
            var property = await _context.Properties.FindAsync(id);
            if (property is null) return NotFound();

            property.Title       = updated.Title;
            property.Description = updated.Description;
            property.Price       = updated.Price;
            property.Location    = updated.Location;
            property.Type        = updated.Type;
            property.Bedrooms    = updated.Bedrooms;
            property.Bathrooms   = updated.Bathrooms;
            property.SquareFeet  = updated.SquareFeet;
            property.IsAvailable = updated.IsAvailable;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE /api/properties/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var property = await _context.Properties.FindAsync(id);
            if (property is null) return NotFound();

            _context.Properties.Remove(property);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}