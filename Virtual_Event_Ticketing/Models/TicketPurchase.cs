using WebApplication1.Models;

public class TicketPurchase
{
    public int Id { get; set; }
    public int EventId { get; set; }
    public Event? Event { get; set; }
    public string BuyerId { get; set; } = null!;
    public ApplicationUser? Buyer { get; set; }
    public int Quantity { get; set; }
    public DateTime PurchasedAt { get; set; }
    public decimal TotalPrice { get; set; }
    public string? QrCodeData { get; set; }
}