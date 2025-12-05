using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _db;

        public DashboardController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            // Get current user ID
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Use UTC for all comparisons
            var now = DateTime.UtcNow;

            // Upcoming tickets (future events)
            var upcomingTickets = await _db.Tickets
                .Include(t => t.Event)
                .Where(t => t.AttendeeId == userId && t.Event.EventDate >= now)
                .ToListAsync();

            // Purchase history (past events)
            var purchaseHistory = await _db.Tickets
                .Include(t => t.Event)
                .Where(t => t.AttendeeId == userId && t.Event.EventDate < now)
                .ToListAsync();

            // My events (only for organizers)
            var myEvents = new List<Event>();
            if (User.IsInRole("Organizer") || User.IsInRole("Admin"))
            {
                myEvents = await _db.Events
                    .Where(e => e.OrganizerId == userId)
                    .Include(e => e.Tickets)
                    .ToListAsync();
            }

            // Create ViewModel
            var model = new DashboardViewModel
            {
                UpcomingTickets = upcomingTickets,
                PurchaseHistory = purchaseHistory,
                MyEvents = myEvents,
                UserProfile = await _db.Users
                    .Where(u => u.Id == userId)
                    .Select(u => new UserProfileViewModel
                    {
                        Name = u.FullName,
                        Email = u.Email,
                        PhoneNumber = u.PhoneNumber,
                        ProfilePicturePath = u.ProfilePicturePath
                    }).FirstOrDefaultAsync()
            };

            return View(model);
        }
    }
}


