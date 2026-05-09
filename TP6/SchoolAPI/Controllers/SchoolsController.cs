using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolAPI.DTOs;
using SchoolAPI.Models;

namespace SchoolAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SchoolsController : ControllerBase
{
    private readonly SchoolDbContext _context;
    private readonly IMapper _mapper;

    public SchoolsController(SchoolDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet("get-all-schools")]
    public async Task<ActionResult<IEnumerable<School>>> GetSchools()
    {
        return await _context.Schools.ToListAsync();
    }

    [HttpGet("get-school-by-id/{id}")]
    public async Task<ActionResult<School>> GetSchool(int id)
    {
        var school = await _context.Schools.FindAsync(id);
        if (school == null) return NotFound();
        return school;
    }

    [HttpGet("search-by-name")]
    public async Task<ActionResult<IEnumerable<School>>> SearchByName(string name)
    {
        var schools = await _context.Schools
            .Where(s => s.Name.Contains(name))
            .ToListAsync();
        return Ok(schools);
    }

    [HttpPost("create-school")]
    public async Task<ActionResult<School>> PostSchool(School school)
    {
        _context.Schools.Add(school);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetSchool), new { id = school.Id }, school);
    }

    [HttpPut("edit-school/{id}")]
    public async Task<IActionResult> PutSchool(int id, School school)
    {
        if (id != school.Id) return BadRequest();
        _context.Entry(school).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("delete-school/{id}")]
    public async Task<IActionResult> DeleteSchool(int id)
    {
        var school = await _context.Schools.FindAsync(id);
        if (school == null) return NotFound();
        _context.Schools.Remove(school);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpGet("list-schools")]
    public ActionResult<IEnumerable<SchoolDto>> ListSchools()
    {
        return Ok(_context.Schools.Select(school => _mapper.Map<SchoolDto>(school)));
    }

    [HttpPost("add-new-school")]
    public async Task<ActionResult> AddSchool(SchoolDto newschool)
    {
        var school = _mapper.Map<School>(newschool);
        _context.Schools.Add(school);
        await _context.SaveChangesAsync();
        return Ok();
    }
}
