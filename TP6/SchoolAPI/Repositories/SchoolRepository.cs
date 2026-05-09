using SchoolAPI.Models;

namespace SchoolAPI.Repositories;

public class SchoolRepository : IUniversityRepository
{
    private readonly SchoolDbContext _context;

    public SchoolRepository(SchoolDbContext context)
    {
        _context = context;
    }

    public IEnumerable<School> GetSchools()
        => _context.Schools.ToList();

    public School GetSchoolById(int id)
        => _context.Schools.Find(id)!;

    public IEnumerable<School> GetSchoolsByName(string name)
        => _context.Schools.Where(s => s.Name.Contains(name)).ToList();

    public void AddSchool(School school)
    {
        _context.Schools.Add(school);
        _context.SaveChanges();
    }

    public void UpdateSchool(School school)
    {
        _context.Entry(school).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        _context.SaveChanges();
    }

    public void DeleteSchool(int id)
    {
        var school = _context.Schools.Find(id);
        if (school != null)
        {
            _context.Schools.Remove(school);
            _context.SaveChanges();
        }
    }
}
