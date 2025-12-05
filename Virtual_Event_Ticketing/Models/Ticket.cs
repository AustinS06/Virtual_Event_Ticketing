namespace WebApplication1.Models;

public class Ticket
{
    public int Id { get; set; }
    public string EventName { get; set; }
    public DateTime EventDate { get; set; }
    public int EventId { get; set; }
    public decimal Price { get; set; }
    public Event Event { get; set; } = null!;
    public string AttendeeId { get; set; } = "";

}