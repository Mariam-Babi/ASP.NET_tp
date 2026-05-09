using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CinemaManager.Data;
using CinemaManager.Models.Cinema;

namespace CinemaManager.Controllers;

public class ProducersController : Controller
{
    private readonly CinemaDbContext _db;

    public ProducersController(CinemaDbContext context)
    {
        _db = context;
    }

    public IActionResult Index()
    {
        var p = _db.Producers.ToList();
        return View(p);
    }

    public async Task<IActionResult> ProdsAndTheirMovies()
    {
        var p = _db.Producers.Include(p => p.Movies);
        return View(await p.ToListAsync());
    }

    public IActionResult ProdsAndTheirMovies_UsingModel()
    {
        var result = from p in _db.Producers
                     join f in _db.Movies
                     on p.Id equals f.ProducerId
                     select new ProdMovie
                     {
                         Id = p.Id,
                         pName = p.Name,
                         pNat = p.Nationality,
                         mTitle = f.Title,
                         mGenre = f.Genre
                     };
        return View(result.ToList());
    }
}
