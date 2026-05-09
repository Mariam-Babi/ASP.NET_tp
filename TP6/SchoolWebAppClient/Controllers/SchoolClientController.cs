using Microsoft.AspNetCore.Mvc;
using SchoolWebAppClient.Models;

namespace SchoolWebAppClient.Controllers;

public class SchoolClientController : Controller
{
    private readonly HttpClient _client;

    public SchoolClientController(IHttpClientFactory factory)
    {
        _client = factory.CreateClient("SchoolAPI");
    }

    public async Task<IActionResult> GetAllSchools()
    {
        var response = await _client.GetAsync("api/SchoolsRepo/get-all-schools");
        if (response.IsSuccessStatusCode)
        {
            var schools = await response.Content.ReadFromJsonAsync<IEnumerable<SchoolClient>>();
            return View(schools);
        }
        return View(Enumerable.Empty<SchoolClient>());
    }

    public async Task<IActionResult> GetSchoolById(int id)
    {
        var response = await _client.GetAsync($"api/SchoolsRepo/get-school-by-id/{id}");
        if (response.IsSuccessStatusCode)
        {
            var school = await response.Content.ReadFromJsonAsync<SchoolClient>();
            return View(school);
        }
        return View();
    }

    public async Task<IActionResult> GetSchoolByName(string name)
    {
        var response = await _client.GetAsync($"api/SchoolsRepo/search-by-name?name={name}");
        if (response.IsSuccessStatusCode)
        {
            var schools = await response.Content.ReadFromJsonAsync<IEnumerable<SchoolClient>>();
            return View(schools);
        }
        return View(Enumerable.Empty<SchoolClient>());
    }

    [HttpGet]
    public IActionResult CreateSchool()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateSchool(SchoolClient school)
    {
        var response = await _client.PostAsJsonAsync("api/SchoolsRepo/create-school", school);
        if (response.IsSuccessStatusCode)
            return RedirectToAction(nameof(GetAllSchools));
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> EditSchool(int id)
    {
        var response = await _client.GetAsync($"api/SchoolsRepo/get-school-by-id/{id}");
        if (response.IsSuccessStatusCode)
        {
            var school = await response.Content.ReadFromJsonAsync<SchoolClient>();
            return View(school);
        }
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> EditSchool(SchoolClient school)
    {
        var response = await _client.PutAsJsonAsync($"api/SchoolsRepo/edit-school/{school.Id}", school);
        if (response.IsSuccessStatusCode)
            return RedirectToAction(nameof(GetAllSchools));
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> DeleteSchool(int id)
    {
        var response = await _client.GetAsync($"api/SchoolsRepo/get-school-by-id/{id}");
        if (response.IsSuccessStatusCode)
        {
            var school = await response.Content.ReadFromJsonAsync<SchoolClient>();
            return View(school);
        }
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> DeleteSchool(SchoolClient school)
    {
        var response = await _client.DeleteAsync($"api/SchoolsRepo/delete-school/{school.Id}");
        if (response.IsSuccessStatusCode)
            return RedirectToAction(nameof(GetAllSchools));
        return View();
    }
}
