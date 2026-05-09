using Microsoft.EntityFrameworkCore;

namespace SchoolAPI.Models;

public class SchoolDbContext : DbContext
{
    public SchoolDbContext(DbContextOptions<SchoolDbContext> options) : base(options) { }

    public virtual DbSet<School> Schools { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<School>().HasData(
            new School
            {
                Id = 1,
                Name = "ENISo",
                Sections = "IA, GTE, GMP",
                Director = "Chayma Ben Ali",
                Rating = 3.5,
                WebSite = "http://www.eniso.rnu.tn"
            },
            new School
            {
                Id = 2,
                Name = "ENIM",
                Sections = "Mécanique, Énergétique, Textile",
                Director = "Chayma Mansouri",
                Rating = 2.8
            },
            new School
            {
                Id = 3,
                Name = "ENIT",
                Sections = "Télécom, Info, Indus",
                Director = "Chayma Trabelsi",
                Rating = 4.0,
                WebSite = "http://www.enit.rnu.tn"
            }
        );
    }
}
