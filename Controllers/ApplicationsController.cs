using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClientConnect.Data;
using ClientConnect.Models;

namespace ClientConnect.Controllers
{
    /// <summary>
    /// Manages application-related operations, including listing all applications,
    /// displaying details, creating new applications, editing existing ones,
    /// and deleting applications submitted by freelancers for projects.
    /// </summary>

    public class ApplicationsController : Controller
    {
        // Reference to the database context
        private readonly AppDbContext _context;

        // Constructor injects the database context
        public ApplicationsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Applications
        // Returns a list of all applications with related freelancer and project data
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Applications
                .Include(a => a.Freelancer)
                .Include(a => a.ProjectListing);

            return View(await appDbContext.ToListAsync());
        }

        // GET: Applications/Details/5
        // Displays detailed information for a specific application
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var application = await _context.Applications
                .Include(a => a.Freelancer)
                .Include(a => a.ProjectListing)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (application == null)
            {
                return NotFound();
            }

            return View(application);
        }

        // GET: Applications/Create
        // Loads the application form for a given project
        public IActionResult Create(int? projectId)
        {
            // Simulated login - hardcoded freelancer ID for demonstration
            var freelancerId = 1;
            var freelancer = _context.Freelancers.Find(freelancerId);

            if (projectId.HasValue)
            {
                var project = _context.ProjectListings.FirstOrDefault(p => p.Id == projectId);
                if (project != null)
                {
                    ViewData["ProjectTitle"] = project.Title;
                }
            }

            ViewBag.FreelancerEmail = freelancer?.Email ?? "Test User";

            var model = new Application
            {
                ProjectListingId = projectId ?? 0,
                FreelancerId = freelancerId,
                SubmittedAt = DateTime.Now
            };

            ViewData["ProjectListingId"] = new SelectList(_context.ProjectListings, "Id", "Title");

            return View(model);
        }

        // POST: Applications/Create
        // Handles submission of a new application
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProjectListingId,FreelancerId,CoverLetter")] Application application)
        {
            // Simulate logged-in freelancer (replace with real authentication later)
            application.FreelancerId = 1;

            // Set submission timestamp
            application.SubmittedAt = DateTime.Now;

            if (ModelState.IsValid)
            {
                _context.Add(application);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["FreelancerId"] = new SelectList(_context.Freelancers, "Id", "Email", application.FreelancerId);
            ViewData["ProjectListingId"] = new SelectList(_context.ProjectListings, "Id", "Title", application.ProjectListingId);

            return View(application);
        }

        // GET: Applications/Edit/5
        // Loads the application for editing
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var application = await _context.Applications.FindAsync(id);
            if (application == null)
            {
                return NotFound();
            }

            ViewData["FreelancerId"] = new SelectList(_context.Freelancers, "Id", "Email", application.FreelancerId);
            ViewData["ProjectListingId"] = new SelectList(_context.ProjectListings, "Id", "ClientName", application.ProjectListingId);

            return View(application);
        }

        // POST: Applications/Edit/5
        // Handles application update
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProjectListingId,FreelancerId,CoverLetter,SubmittedAt")] Application application)
        {
            if (id != application.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(application);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicationExists(application.Id))
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

            ViewData["FreelancerId"] = new SelectList(_context.Freelancers, "Id", "Email", application.FreelancerId);
            ViewData["ProjectListingId"] = new SelectList(_context.ProjectListings, "Id", "ClientName", application.ProjectListingId);

            return View(application);
        }

        // GET: Applications/Delete/5
        // Loads a confirmation page for deleting an application
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var application = await _context.Applications
                .Include(a => a.Freelancer)
                .Include(a => a.ProjectListing)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (application == null)
            {
                return NotFound();
            }

            return View(application);
        }

        // POST: Applications/Delete/5
        // Handles the deletion of the application after confirmation
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var application = _context.Applications.FirstOrDefault(a => a.Id == id);

            if (application != null)
            {
                _context.Applications.Remove(application);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }

        // Helper method to check if an application exists
        private bool ApplicationExists(int id)
        {
            return _context.Applications.Any(e => e.Id == id);
        }
    }
}
