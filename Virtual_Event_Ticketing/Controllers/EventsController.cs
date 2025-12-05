using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;

namespace WebApplication1.Controllers;

public class EventsController : Controller
{
    private readonly ApplicationDbContext _db;
    public EventsController(ApplicationDbContext db) { _db = db; }


    public async Task<IActionResult> Index()
    {
        var events = await _db.Events
            .OrderBy(e => e.StartDate)
            .ToListAsync();

        return View(events);
    }


// AJAX search endpoint returning a partial view
    [HttpGet]
    public async Task<IActionResult> Search(string search)
    {
        if (string.IsNullOrEmpty(search))
        {
            // Return all events if search is empty
            var allEvents = await _db.Events.ToListAsync();
            return PartialView("_EventListPartial", allEvents);
        }

        var results = await _db.Events
            .Where(e => e.Title.Contains(search))
            .ToListAsync();

        return PartialView("_EventListPartial", results);
    }

}