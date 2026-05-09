using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CinemaManager.Data;
using CinemaManager.Models.Cinema;

namespace CinemaManager.Controllers;

public class MoviesController : Controller
{
    private readonly CinemaDbContext _db;

    public MoviesController(CinemaDbContext context)
    {
        _db = context;
    }

    public async Task<IActionResult> Index()
    {
        var f = _db.Movies.Include(f => f.Producer);
        return View(await f.ToListAsync());
    }

    public async Task<IActionResult> MoviesAndTheirProds()
    {
        var f = _db.Movies.Include(f => f.Producer);
        return View(await f.ToListAsync());
    }

    public IActionResult MoviesAndTheirProds_UsingModel()
    {
        var result = from f in _db.Movies
                     join p in _db.Producers
                     on f.ProducerId equals p.Id
                     select new ProdMovie
                     {
                         mTitle = f.Title,
                         mGenre = f.Genre,
                         pName = p.Name,
                         pNat = p.Nationality
                     };
        return View(result.ToList());
    }

    public IActionResult MyMovies(int id)
    {
        var result = from f in _db.Movies
                     join p in _db.Producers
                     on f.ProducerId equals p.Id
                     where p.Id == id
                     select new ProdMovie
                     {
                         mTitle = f.Title,
                         mGenre = f.Genre,
                         pName = p.Name,
                         pNat = p.Nationality
                     };
        return View(result.ToList());
    }

    public IActionResult SearchByTitle(string SearchTerm)
    {
        var f = from m in _db.Movies.Include(m => m.Producer)
                select m;

        if (!string.IsNullOrEmpty(SearchTerm))
        {
            f = f.Where(m => m.Title.Contains(SearchTerm));
        }

        return View(f.ToList());
    }

    public IActionResult SearchByGenre(string SearchTerm)
    {
        var f = from m in _db.Movies.Include(m => m.Producer)
                select m;

        if (!string.IsNullOrEmpty(SearchTerm))
        {
            f = f.Where(m => m.Genre.Contains(SearchTerm));
        }

        return View(f.ToList());
    }

    public IActionResult SearchBy2(string selectedGenre, string SearchTerm)
    {
        var genreList = _db.Movies
                           .Select(f => f.Genre)
                           .Distinct()
                           .ToList();
        genreList.Insert(0, "All");
        ViewBag.Genres = new SelectList(genreList);

        var f = _db.Movies.Include(f => f.Producer).AsQueryable();

        if (!string.IsNullOrEmpty(SearchTerm))
        {
            f = f.Where(m => m.Title.Contains(SearchTerm));
        }

        if (!string.IsNullOrEmpty(selectedGenre) && selectedGenre != "All")
        {
            f = f.Where(m => m.Genre == selectedGenre);
        }

        return View(f.ToList());
    }
}
