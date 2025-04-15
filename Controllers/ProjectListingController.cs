using Microsoft.AspNetCore.Mvc;
using ClientConnect.Models;
using ClientConnect.Data;
using System;
using Microsoft.EntityFrameworkCore;

namespace ClientConnect.Controllers
{
    /// <summary>
    /// Controls all actions related to project listings, including displaying available projects,
    /// showing project details, creating and managing postings, and deleting project opportunities.
    /// </summary>
    public class ProjectListingController : Controller
    {
        // Injected database context to access the database
        private readonly AppDbContext _context;

        // Constructor to initialize the context
        public ProjectListingController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /ProjectListing/
        // Show all project listings
        public IActionResult Index()
        {
            var projects = _context.ProjectListings.ToList();
            return View(projects);
        }

        // GET: ProjectListing/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProjectListing/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProjectListing projectListing)
        {
            if (ModelState.IsValid)
            {
                _context.Add(projectListing);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // If the model is invalid, redisplay the form with error messages
            return View(projectListing);
        }

        // GET: ProjectListing/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectListing = await _context.ProjectListings
                .FirstOrDefaultAsync(p => p.Id == id);

            if (projectListing == null)
            {
                return NotFound();
            }

            return View(projectListing);
        }

        // GET: ProjectListing/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.ProjectListings.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }
            return View(project);
        }

        // POST: ProjectListing/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Budget,ClientName")] ProjectListing project)
        {
            if (id != project.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(project);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.ProjectListings.Any(e => e.Id == id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(project);
        }

        // GET: ProjectListing/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.ProjectListings
                .FirstOrDefaultAsync(m => m.Id == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // POST: ProjectListing/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var project = await _context.ProjectListings.FindAsync(id);

            if (project != null)
            {
                _context.ProjectListings.Remove(project);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
