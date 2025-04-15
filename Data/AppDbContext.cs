using Microsoft.EntityFrameworkCore;
using ClientConnect.Models;
using Microsoft.Extensions.Options;
using System.Runtime.Intrinsics.X86;

namespace ClientConnect.Data
{
    /// <summary>
    /// Represents the Entity Framework database context for the ClientConnect application.
    /// Manages the database connection and provides access to the application's entities.
    /// </summary>
    public class AppDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppDbContext"/> class using the specified options.
        /// </summary>
        /// <param name="options">The options to be used by the DbContext.</param>
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Gets or sets the Clients table.
        public DbSet<Client> Clients { get; set; }

        // Gets or sets the Freelancers table.
        public DbSet<Freelancer> Freelancers { get; set; }

        // Gets or sets the ProjectListings table.
        public DbSet<ProjectListing> ProjectListings { get; set; }

        // Gets or sets the Applications table (submitted by freelancers for project listings).
        public DbSet<Application> Applications { get; set; }
    }
}

