using WebApplication1.Data;
using WebApplication1.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
    {
        options.SignIn.RequireConfirmedEmail = false;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders()
    .AddDefaultUI(); 

builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();  
app.UseAuthorization();

// Map default route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    
    db.Database.EnsureCreated();

    
    if (!db.Events.Any())
    {
        db.Events.AddRange(
            new Event
            {
                Title = "Music Festival",
                Description = "An amazing festival with live music and local bands.",
                Category = "Music",
                Location = "Central Park",
                StartDate = DateTime.UtcNow.AddDays(30),
                EventDate = DateTime.UtcNow.AddDays(30),
                TotalTickets = 500,
                TicketsSold = 0,
                Price = 99.99m,
                Organizer = "EventMaster"
            },
            new Event
            {
                Title = "Tech Conference 2026",
                Description = "The latest in technology and innovation, with industry leaders speaking.",
                Category = "Technology",
                Location = "Convention Center",
                StartDate = DateTime.UtcNow.AddDays(60),
                EventDate = DateTime.UtcNow.AddDays(60),
                TotalTickets = 300,
                TicketsSold = 0,
                Price = 199.99m,
                Organizer = "TechCorp"
            },
            new Event
            {
                Title = "Art Expo",
                Description = "Explore contemporary art from around the world.",
                Category = "Art",
                Location = "Downtown Gallery",
                StartDate = DateTime.UtcNow.AddDays(45),
                EventDate = DateTime.UtcNow.AddDays(45),
                TotalTickets = 150,
                TicketsSold = 0,
                Price = 49.99m,
                Organizer = "ArtWorld"
            },
            new Event
            {
                Title = "Food Carnival",
                Description = "Taste dishes from top chefs and local food vendors.",
                Category = "Food & Drink",
                Location = "City Square",
                StartDate = DateTime.UtcNow.AddDays(20),
                EventDate = DateTime.UtcNow.AddDays(20),
                TotalTickets = 400,
                TicketsSold = 0,
                Price = 29.99m,
                Organizer = "FoodiesUnited"
            },
            new Event
            {
                Title = "Charity Run",
                Description = "Join a 5k run to support local charities.",
                Category = "Sports",
                Location = "Riverside Park",
                StartDate = DateTime.UtcNow.AddDays(15),
                EventDate = DateTime.UtcNow.AddDays(15),
                TotalTickets = 250,
                TicketsSold = 0,
                Price = 15.00m,
                Organizer = "RunForGood"
            }
        );

        db.SaveChanges();
    }
}



app.Run();