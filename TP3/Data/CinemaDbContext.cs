using Microsoft.EntityFrameworkCore;
using CinemaManager.Models.Cinema;

namespace CinemaManager.Data;

public class CinemaDbContext : DbContext
{
    public CinemaDbContext(DbContextOptions<CinemaDbContext> options) : base(options) { }

    public DbSet<Movie> Movies { get; set; }
    public DbSet<Producer> Producers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Movie>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).IsRequired();
            entity.Property(e => e.Genre).IsRequired();
            entity.HasOne(e => e.Producer)
                  .WithMany(p => p.Movies)
                  .HasForeignKey(e => e.ProducerId);
        });

        modelBuilder.Entity<Producer>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired();
            entity.Property(e => e.Nationality).IsRequired();
            entity.Property(e => e.Email).IsRequired();
        });
    }
}
