using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;

namespace WebApplication1.Controllers;

public class EventsController(ApplicationDbContext db) : Controller
{
    
    
    public async Task<IActionResult> Index(string search)
    {
        var query = db.Events.AsQueryable();

        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(e =>
                e.Title.Contains(search) ||
                e.Description.Contains(search) ||
                e.Category.Contains(search) ||
                e.Location.Contains(search)
            );
        }

        var eventsList = await query
            .OrderBy(e => e.StartDate)
            .ToListAsync();

        return View(eventsList);
    }

    
    public async Task<IActionResult> Details(int id)
    {
        var ev = await db.Events
            .FirstOrDefaultAsync(e => e.Id == id);

        if (ev == null)
            return NotFound();

        return View(ev);
    }




    [HttpGet]
    public async Task<IActionResult> Search(string search)
    {
        if (string.IsNullOrEmpty(search))
        {
            
            var allEvents = await db.Events.ToListAsync();
            return PartialView("_EventListPartial", allEvents);
        }

        var results = await db.Events
            .Where(e => e.Title.Contains(search))
            .ToListAsync();

        return PartialView("_EventListPartial", results);
    }

    public async Task<IActionResult> Past()
    {
        var now = DateTime.UtcNow;

        var pastEvents = await db.Events
            .Where(e => e.StartDate < now)
            .OrderByDescending(e => e.StartDate)
            .ToListAsync();

        return View(pastEvents);
    }
}
