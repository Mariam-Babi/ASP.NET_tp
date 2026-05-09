using Microsoft.AspNetCore.Mvc;
using SchoolAPI.Models;
using SchoolAPI.Repositories;

namespace SchoolAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SchoolsRepoController : ControllerBase
{
    private readonly IUniversityRepository _repo;

    public SchoolsRepoController(IUniversityRepository repo)
    {
        _repo = repo;
    }

    [HttpGet("get-all-schools")]
    public ActionResult<IEnumerable<School>> GetSchools()
        => Ok(_repo.GetSchools());

    [HttpGet("get-school-by-id/{id}")]
    public ActionResult<School> GetSchool(int id)
    {
        var school = _repo.GetSchoolById(id);
        if (school == null) return NotFound();
        return Ok(school);
    }

    [HttpGet("search-by-name")]
    public ActionResult<IEnumerable<School>> SearchByName(string name)
        => Ok(_repo.GetSchoolsByName(name));

    [HttpPost("create-school")]
    public ActionResult PostSchool(School school)
    {
        _repo.AddSchool(school);
        return Ok();
    }

    [HttpPut("edit-school/{id}")]
    public ActionResult PutSchool(int id, School school)
    {
        if (id != school.Id) return BadRequest();
        _repo.UpdateSchool(school);
        return NoContent();
    }

    [HttpDelete("delete-school/{id}")]
    public ActionResult DeleteSchool(int id)
    {
        _repo.DeleteSchool(id);
        return NoContent();
    }
}
