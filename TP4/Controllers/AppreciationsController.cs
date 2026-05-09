using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hotellerie_X.Models.HotellerieModel;

namespace Hotellerie_X.Controllers
{
    public class AppreciationsController : Controller
    {
        private readonly HotellerieDbContext _context;

        public AppreciationsController(HotellerieDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var appreciations = _context.Appreciations.Include(a => a.Hotel);
            return View(await appreciations.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var appreciation = await _context.Appreciations
                .Include(a => a.Hotel)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (appreciation == null) return NotFound();

            return View(appreciation);
        }

        public IActionResult Create()
        {
            ViewData["HotelId"] = new SelectList(_context.Hotels, "Id", "Nom");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NomPers,Commentaire,Note,HotelId")] Appreciation appreciation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(appreciation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["HotelId"] = new SelectList(_context.Hotels, "Id", "Nom", appreciation.HotelId);
            return View(appreciation);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var appreciation = await _context.Appreciations.FindAsync(id);

            if (appreciation == null) return NotFound();

            ViewData["HotelId"] = new SelectList(_context.Hotels, "Id", "Nom", appreciation.HotelId);
            return View(appreciation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NomPers,Commentaire,Note,HotelId")] Appreciation appreciation)
        {
            if (id != appreciation.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appreciation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Appreciations.Any(e => e.Id == appreciation.Id)) return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["HotelId"] = new SelectList(_context.Hotels, "Id", "Nom", appreciation.HotelId);
            return View(appreciation);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var appreciation = await _context.Appreciations
                .Include(a => a.Hotel)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (appreciation == null) return NotFound();

            return View(appreciation);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var appreciation = await _context.Appreciations.FindAsync(id);
            if (appreciation != null) _context.Appreciations.Remove(appreciation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
