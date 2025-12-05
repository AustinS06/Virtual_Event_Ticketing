namespace WebApplication1.Models
{
    public class DashboardViewModel
    {
        public List<Ticket> UpcomingTickets { get; set; } = new();
        public List<Ticket> PurchaseHistory { get; set; } = new();
        public List<Event> MyEvents { get; set; } = new();
        public UserProfileViewModel UserProfile { get; set; } = new();
    }

    public class UserProfileViewModel
    {
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
        public string PhoneNumber { get; set; } = "";
        public string ProfilePicturePath { get; set; } = "";
    }
}

