using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RestoManager_X.Models.RestosModel;

namespace RestoManager_X.Controllers
{
    public class AvisController : Controller
    {
        private readonly RestosDbContext _context;

        public AvisController(RestosDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var avis = _context.Avis.Include(a => a.LeResto);
            return View(await avis.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var avis = await _context.Avis
                .Include(a => a.LeResto)
                .FirstOrDefaultAsync(m => m.CodeAvis == id);

            if (avis == null) return NotFound();

            return View(avis);
        }

        public IActionResult Create()
        {
            ViewData["NumResto"] = new SelectList(_context.Restaurants, "CodeResto", "NomResto");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CodeAvis,NomPersonne,Note,Commentaire,NumResto")] Avis avis)
        {
            if (ModelState.IsValid)
            {
                _context.Add(avis);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["NumResto"] = new SelectList(_context.Restaurants, "CodeResto", "NomResto", avis.NumResto);
            return View(avis);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var avis = await _context.Avis.FindAsync(id);

            if (avis == null) return NotFound();

            ViewData["NumResto"] = new SelectList(_context.Restaurants, "CodeResto", "NomResto", avis.NumResto);
            return View(avis);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CodeAvis,NomPersonne,Note,Commentaire,NumResto")] Avis avis)
        {
            if (id != avis.CodeAvis) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(avis);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Avis.Any(e => e.CodeAvis == avis.CodeAvis)) return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["NumResto"] = new SelectList(_context.Restaurants, "CodeResto", "NomResto", avis.NumResto);
            return View(avis);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var avis = await _context.Avis
                .Include(a => a.LeResto)
                .FirstOrDefaultAsync(m => m.CodeAvis == id);

            if (avis == null) return NotFound();

            return View(avis);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var avis = await _context.Avis.FindAsync(id);
            if (avis != null) _context.Avis.Remove(avis);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
