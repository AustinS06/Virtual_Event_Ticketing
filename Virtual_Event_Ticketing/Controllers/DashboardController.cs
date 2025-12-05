using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class DashboardController(ApplicationDbContext db) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var now = DateTime.UtcNow;

            
            var upcomingTickets = await db.Tickets
                .Include(t => t.Event)
                .Where(t => t.AttendeeId == userId && t.Event.EventDate >= now)
                .ToListAsync();

            
            var purchaseHistory = await db.Tickets
                .Include(t => t.Event)
                .Where(t => t.AttendeeId == userId && t.Event.EventDate < now)
                .ToListAsync();

           
            List<Event> myEvents = new();
            if (User.IsInRole("Organizer") || User.IsInRole("Admin"))
            {
                myEvents = await db.Events
                    .Where(e => e.OrganizerId == userId)
                    .Include(e => e.Tickets)
                    .ToListAsync();
            }

            
            var upcomingEvents = await db.Events
                .Where(e => e.EventDate >= now)
                .OrderBy(e => e.EventDate)
                .Take(5)
                .ToListAsync();

           
            var user = await db.Users.FirstOrDefaultAsync(u => u.Id == userId);

            var model = new DashboardViewModel
            {
                FullName = user?.FullName ?? "",
                Email = user?.Email ?? "",
                PhoneNumber = user?.PhoneNumber ?? "",
                

                UpcomingTickets = upcomingTickets,
                PurchaseHistory = purchaseHistory,
                MyEvents = myEvents,
                UpcomingEvents = upcomingEvents
            };

            return View(model);
        }
    }
}



