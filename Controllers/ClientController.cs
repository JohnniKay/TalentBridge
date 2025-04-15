using Microsoft.AspNetCore.Mvc;
using ClientConnect.Models;
using ClientConnect.Data;
using System;

namespace ClientConnect.Controllers
{
    /// <summary>
    /// Manages client-related operations, including listing client profiles,
    /// viewing client information, creating new clients, editing client details,
    /// and removing client accounts from the system.
    /// </summary>
    public class ClientController : Controller
    {
        // Database context injected for accessing client data
        private readonly AppDbContext _context;

        // Constructor initializes the controller with the database context
        public ClientController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /Client/
        // Show all clients in a list
        public IActionResult Index()
        {
            var clients = _context.Clients.ToList();
            return View(clients);
        }
    }
}
