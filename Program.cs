/*
   Summary:
   File: Program.cs
   Purpose: Entry point for the ClientConnect web application.
   Responsibilities:
   - Configures services including MVC and the in-memory database (ClientConnectDB)
   - Sets up the HTTP request pipeline with middleware for error handling, static files, routing, etc.
   - Seeds the application with sample data at startup using SeedData.Initialize()

   Notes:
   - Uses InMemoryDatabase for quick prototyping. For production, consider switching to a persistent database like SQL Server.
*/

using ClientConnect.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure the database (currently using in-memory for development/demo)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("ClientConnectDB")); // Switch to UseSqlServer(...) for production DB

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // Use a custom error handler in production
    app.UseExceptionHandler("/Home/Error");

    // Enable HSTS for security in production environments
    app.UseHsts();
}

// Enforce HTTPS redirection
app.UseHttpsRedirection();

// Enable serving static files (e.g., CSS, JS, images)
app.UseStaticFiles();

app.UseRouting();

// Authorization middleware (if using authentication later)
app.UseAuthorization();

// Define default route pattern for MVC
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Seed database with initial data
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    SeedData.Initialize(services);
}

// Run the application
app.Run();
