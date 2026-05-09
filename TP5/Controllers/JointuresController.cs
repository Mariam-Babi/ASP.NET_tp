using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestoManager_X.Models.RestosModel;

namespace RestoManager_X.Controllers
{
    public class JointuresController : Controller
    {
        private readonly RestosDbContext _context;

        public JointuresController(RestosDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> AvisParRestaurant()
        {
            var restaurants = await _context.Restaurants
                .Include(r => r.LesAvis)
                .ToListAsync();

            return View(restaurants);
        }

        public async Task<IActionResult> AvisParCode(int? code)
        {
            if (code == null)
            {
                var tous = await _context.Restaurants.ToListAsync();
                ViewBag.Restaurants = tous;
                return View(new List<Avis>());
            }

            var avis = await _context.Avis
                .Where(a => a.NumResto == code)
                .Include(a => a.LeResto)
                .ToListAsync();

            var restaurant = await _context.Restaurants.FindAsync(code);
            ViewBag.RestaurantNom = restaurant?.NomResto ?? "Inconnu";
            ViewBag.CodeResto = code;
            ViewBag.Restaurants = await _context.Restaurants.ToListAsync();

            return View(avis);
        }

        public async Task<IActionResult> RestaurantsBonneNote()
        {
            var restaurants = await _context.Restaurants
                .Include(r => r.LesAvis)
                .Where(r => r.LesAvis != null && r.LesAvis.Any())
                .ToListAsync();

            var result = restaurants
                .Where(r => r.LesAvis!.Average(a => a.Note) >= 3.5)
                .Select(r => new
                {
                    Restaurant = r,
                    Moyenne = r.LesAvis!.Average(a => a.Note)
                })
                .ToList();

            ViewBag.Data = result.Select(x => new {
                x.Restaurant.CodeResto,
                x.Restaurant.NomResto,
                x.Restaurant.Ville,
                x.Restaurant.Specialite,
                Moyenne = Math.Round(x.Moyenne, 2),
                NbAvis = x.Restaurant.LesAvis!.Count
            }).ToList();

            return View(result.Select(x => x.Restaurant).ToList());
        }
    }
}
