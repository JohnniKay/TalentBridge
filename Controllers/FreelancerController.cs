using Microsoft.AspNetCore.Mvc;
using ClientConnect.Data;
using ClientConnect.Models;
using System.Linq;

namespace ClientConnect.Controllers
{
    /// <summary>
    /// Handles actions related to freelancer profiles, such as listing freelancers,
    /// viewing profile details, creating and editing profiles, and deleting freelancer records.
    /// </summary>
    public class FreelancerController : Controller
    {
        private readonly AppDbContext _context;

        public FreelancerController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /Freelancer/
        public IActionResult Index()
        {
            var freelancers = _context.Freelancers.ToList();
            return View(freelancers);
        }

        // GET: /Freelancer/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Freelancer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Freelancer freelancer)
        {
            if (ModelState.IsValid)
            {
                _context.Freelancers.Add(freelancer);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(freelancer);
        }

        // GET: Freelancer/Details/5
        public IActionResult Details(int id)
        {
            var freelancer = _context.Freelancers.Find(id);
            if (freelancer == null)
            {
                return NotFound();
            }
            return View(freelancer);
        }

        // GET: Freelancer/Edit/5
        public IActionResult Edit(int id)
        {
            var freelancer = _context.Freelancers.Find(id);
            if (freelancer == null)
            {
                return NotFound();
            }
            return View(freelancer);
        }

        // POST: Freelancer/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Freelancer freelancer)
        {
            if (id != freelancer.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(freelancer);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(freelancer);
        }

        // GET: Freelancer/Delete/5
        public IActionResult Delete(int id)
        {
            var freelancer = _context.Freelancers.Find(id);
            if (freelancer == null)
            {
                return NotFound();
            }
            return View(freelancer);
        }

        // POST: Freelancer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var freelancer = _context.Freelancers.Find(id);
            if (freelancer != null)
            {
                _context.Freelancers.Remove(freelancer);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

