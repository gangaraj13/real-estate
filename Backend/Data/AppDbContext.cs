using Microsoft.EntityFrameworkCore;
using Backend.Models;

namespace Backend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User>     Users      { get; set; }
        public DbSet<Agent>    Agents     { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<Favorite> Favorites  { get; set; }
        public DbSet<Inquiry>  Inquiries  { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // --- Users ---
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email).IsUnique();

            // --- Agents ---
            modelBuilder.Entity<Agent>()
                .HasIndex(a => a.Email).IsUnique();

            // --- Properties ---
            modelBuilder.Entity<Property>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Property>()
                .HasOne(p => p.Agent)
                .WithMany(a => a.Properties)
                .HasForeignKey(p => p.AgentId)
                .OnDelete(DeleteBehavior.NoAction);

            // --- Favorites ---
            // Unique constraint: one user can only favorite a property once
            modelBuilder.Entity<Favorite>()
                .HasIndex(f => new { f.UserId, f.PropertyId }).IsUnique();

            modelBuilder.Entity<Favorite>()
                .HasOne(f => f.User)
                .WithMany(u => u.Favorites)
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Favorite>()
                .HasOne(f => f.Property)
                .WithMany(p => p.Favorites)
                .HasForeignKey(f => f.PropertyId)
                .OnDelete(DeleteBehavior.Cascade);

            // --- Inquiries ---
            // Unique constraint: one user can only inquire about a property once
            modelBuilder.Entity<Inquiry>()
                .HasIndex(i => new { i.UserId, i.PropertyId }).IsUnique();

            modelBuilder.Entity<Inquiry>()
                .HasOne(i => i.User)
                .WithMany(u => u.Inquiries)
                .HasForeignKey(i => i.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Inquiry>()
                .HasOne(i => i.Property)
                .WithMany(p => p.Inquiries)
                .HasForeignKey(i => i.PropertyId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Inquiry>()
                .HasOne(i => i.Agent)
                .WithMany(a => a.Inquiries)
                .HasForeignKey(i => i.AgentId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}