
using Microsoft.AspNetCore.Mvc;
using TP2.Models.Cinema;

namespace CinemaManager.Controllers
{
    public class ProducersController : Controller
    {
        CinemaDbContext _context;

        public ProducersController(CinemaDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var producers = _context.Producers.ToList();
            return View(producers);
        }

        public IActionResult Details(int id)
        {
            var producer = _context.Producers.Find(id);
            return View(producer);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Producer producer)
        {
            _context.Producers.Add(producer);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var producer = _context.Producers.Find(id);
            return View(producer);
        }

        [HttpPost]
        public IActionResult Edit(int id, Producer producer)
        {
            _context.Producers.Update(producer);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var producer = _context.Producers.Find(id);
            return View(producer);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var producer = _context.Producers.Find(id);
            _context.Producers.Remove(producer);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult ProdsAndTheirMovies()
        {
            var producers = _context.Producers.Include(p => p.Movies).ToList();
            return View(producers);
        }
        public IActionResult ProdsAndTheirMovies_UsingModel()
        {
            var result = from p in _context.Producers
                         join m in _context.Movies
                         on p.Id equals m.ProducerId
                         select new ProdMovie
                         {
                             pName = p.Name,
                             pNat = p.Nationality,
                             mTitle = m.Title,
                             mGenre = m.Genre
                         };
            return View(result.ToList());
        }
    }
}