using ClientConnect.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace ClientConnect.Data
{
    /// <summary>
    /// Provides a static method to initialize and seed the database with sample data.
    /// This includes Clients, Freelancers, and Project Listings for testing and demonstration purposes.
    /// </summary>
    public static class SeedData
    {
        /// <summary>
        /// Seeds the database with initial test data if it hasn't already been populated.
        /// </summary>
        /// <param name="serviceProvider">Provides access to the configured services, including DbContext options.</param>
        public static void Initialize(IServiceProvider serviceProvider)
        {
            // Create a new instance of the AppDbContext using the service provider
            using var context = new AppDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>());

            // Uncomment this section to prevent reseeding if data already exists
            // if (context.Clients.Any() || context.Freelancers.Any() || context.ProjectListings.Any())
            //     return;

            // Seed sample Clients
            var clients = new[]
            {
                new Client { Name = "Alyssa Tran", Company = "Nimbus Strategies", Email = "sarah@salesco.com" },
                new Client { Name = "Marcus Delgado", Company = "IronPeak Solutions", Email = "info@techtitans.io" },
                new Client { Name = "Janelle Rivers", Company = "BrightForge Consulting", Email = "contact@brightfuture.org" }
            };
            context.Clients.AddRange(clients);
            context.SaveChanges();

            // Seed sample Freelancers
            var freelancers = new[]
            {
                new Freelancer { Name = "John Doe", Email = "john@example.com", Skills = "Sales, CRM", PhoneNumber = "(312) 555-0198", ExperienceYears = 12, IsAvailable = true, Bio = "Passionate full-stack developer with 5+ years of experience building scalable web applications. I thrive in fast-paced environments and enjoy solving complex problems with clean, efficient code." },
                new Freelancer { Name = "Jane Smith", Email = "jane@devmail.com", Skills = "Ruby on Rails, SEO", PhoneNumber = "(646) 555-02738", ExperienceYears = 6, IsAvailable = true, Bio = "Digital marketing expert and SEO strategist with a knack for boosting online visibility. I’ve helped over 30 businesses grow their online presence through data-driven campaigns and creative storytelling." },
                new Freelancer { Name = "Mike Tanaka", Email = "mike@freelancehub.net", Skills = "Full-stack Dev, React", PhoneNumber = "(415) 555-0842", ExperienceYears = 15, IsAvailable = true, Bio = "Seasoned sales consultant with a strong background in B2B SaaS. I love connecting with clients and crafting tailored solutions that drive results. Let’s build something impactful together." }
            };
            context.Freelancers.AddRange(freelancers);
            context.SaveChanges();

            // Seed sample Project Listings, linking to seeded Clients
            var projects = new[]
            {
                new ProjectListing
                {
                    Title = "CRM Optimization Project",
                    Description = "Looking for a freelancer to optimize our Salesforce CRM workflow.",
                    Budget = 1500,
                    ClientId = clients[0].Id,
                    ClientName = clients[0].Name
                },
                new ProjectListing
                {
                    Title = "Landing Page Redesign",
                    Description = "Need a dev to revamp our product landing page.",
                    Budget = 800,
                    ClientId = clients[1].Id,
                    ClientName = clients[1].Name
                },
                new ProjectListing
                {
                    Title = "Email Marketing Funnel",
                    Description = "Help us build an email automation funnel in ActiveCampaign.",
                    Budget = 600,
                    ClientId = clients[2].Id,
                    ClientName = clients[2].Name
                }
            };
            context.ProjectListings.AddRange(projects);
            context.SaveChanges();
        }
    }
}
