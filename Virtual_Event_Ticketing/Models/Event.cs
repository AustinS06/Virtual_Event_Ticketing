using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models;
public class Event
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;
    public string Description { get; set; }
    public string Category { get; set; }  = "";
    public DateTime StartDate { get; set; }

    public string Location { get; set; }
    public DateTime EventDate { get; set; }

    public int TotalTickets { get; set; }
    public int TicketsSold { get; set; }

    public decimal Price { get; set; }
    
    public string OrganizerId { get; set; }
    public string Organizer { get; set; } = null!;

    public List<Ticket> Tickets { get; set; } = new List<Ticket>();
}
