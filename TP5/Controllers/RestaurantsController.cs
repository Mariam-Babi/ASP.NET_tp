using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RestoManager_X.Models.RestosModel;

namespace RestoManager_X.Controllers
{
    public class RestaurantsController : Controller
    {
        private readonly RestosDbContext _context;

        public RestaurantsController(RestosDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var restaurants = _context.Restaurants.Include(r => r.LeProprio);
            return View(await restaurants.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var restaurant = await _context.Restaurants
                .Include(r => r.LeProprio)
                .Include(r => r.LesAvis)
                .FirstOrDefaultAsync(m => m.CodeResto == id);

            if (restaurant == null) return NotFound();

            return View(restaurant);
        }

        public IActionResult Create()
        {
            ViewData["NumProp"] = new SelectList(_context.Proprietaires, "Numero", "Nom");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CodeResto,NomResto,Ville,Tel,NumProp")] Restaurant restaurant)
        {
            if (ModelState.IsValid)
            {
                _context.Add(restaurant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["NumProp"] = new SelectList(_context.Proprietaires, "Numero", "Nom", restaurant.NumProp);
            return View(restaurant);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var restaurant = await _context.Restaurants.FindAsync(id);

            if (restaurant == null) return NotFound();

            ViewData["NumProp"] = new SelectList(_context.Proprietaires, "Numero", "Nom", restaurant.NumProp);
            return View(restaurant);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CodeResto,NomResto,Specialite,Ville,Tel,NumProp")] Restaurant restaurant)
        {
            if (id != restaurant.CodeResto) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(restaurant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Restaurants.Any(e => e.CodeResto == restaurant.CodeResto)) return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["NumProp"] = new SelectList(_context.Proprietaires, "Numero", "Nom", restaurant.NumProp);
            return View(restaurant);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var restaurant = await _context.Restaurants
                .Include(r => r.LeProprio)
                .FirstOrDefaultAsync(m => m.CodeResto == id);

            if (restaurant == null) return NotFound();

            return View(restaurant);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var restaurant = await _context.Restaurants.FindAsync(id);
            if (restaurant != null) _context.Restaurants.Remove(restaurant);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
