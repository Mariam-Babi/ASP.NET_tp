using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hotellerie_X.Models.HotellerieModel;

namespace Hotellerie_X.Controllers
{
    public class HotelsController : Controller
    {
        private readonly HotellerieDbContext _context;

        public HotelsController(HotellerieDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Hotels.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var hotel = await _context.Hotels
                .Include(h => h.Appreciations)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (hotel == null) return NotFound();

            return View(hotel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nom,Etoiles,Ville,SiteWeb,Tel,Pays")] Hotel hotel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hotel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(hotel);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var hotel = await _context.Hotels.FindAsync(id);

            if (hotel == null) return NotFound();

            return View(hotel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nom,Etoiles,Ville,SiteWeb,Tel,Pays")] Hotel hotel)
        {
            if (id != hotel.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hotel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Hotels.Any(e => e.Id == hotel.Id)) return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(hotel);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var hotel = await _context.Hotels.FirstOrDefaultAsync(m => m.Id == id);

            if (hotel == null) return NotFound();

            return View(hotel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hotel = await _context.Hotels.FindAsync(id);
            if (hotel != null) _context.Hotels.Remove(hotel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
