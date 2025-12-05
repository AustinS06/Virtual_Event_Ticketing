using WebApplication1.Models;

namespace WebApplication1.Services
{
    public interface IEventService
    {
        Task<IEnumerable<Event>> GetUpcomingEventsAsync();
        Task<IEnumerable<Event>> GetOrganizerEventsAsync(string organizerId);
        Task<Event?> GetEventByIdAsync(int id);
    }
}