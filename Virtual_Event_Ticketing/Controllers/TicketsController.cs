using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using Microsoft.AspNetCore.Identity;

public class TicketsController : Controller
{
    private readonly ApplicationDbContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public TicketsController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
    {
        _db = db;
        _userManager = userManager;
    }

    
    [HttpGet]
    public async Task<IActionResult> Buy(int id)
    {
        var ev = await _db.Events.FindAsync(id);
        if (ev == null) return NotFound();

        return View(ev); 
    }

    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Buy(int id, int quantity)
    {
        var ev = await _db.Events.FindAsync(id);
        if (ev == null) return NotFound();

        if (quantity <= 0 || quantity > (ev.TotalTickets - ev.TicketsSold))
        {
            ModelState.AddModelError("", "Invalid ticket quantity.");
            return View(ev);
        }

        var user = await _userManager.GetUserAsync(User);
        if (user == null) return Challenge();

        var ticket = new Ticket
        {
            EventId = ev.Id,
            EventName = ev.Title,
            BuyerId = user.Id,
            Quantity = quantity,
            PurchaseDate = DateTime.UtcNow,
            TotalPrice = ev.Price * quantity
        };

        ev.TicketsSold += quantity;

        _db.Tickets.Add(ticket);
        _db.Events.Update(ev);
        await _db.SaveChangesAsync();

        return RedirectToAction("MyTickets");
    }

   
    [HttpGet]
    public async Task<IActionResult> MyTickets()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return Challenge();

        var tickets = await _db.Tickets
            .Include(t => t.Event)
            .Where(t => t.BuyerId == user.Id)
            .OrderByDescending(t => t.PurchaseDate)
            .ToListAsync();

        return View(tickets);
    }
}





