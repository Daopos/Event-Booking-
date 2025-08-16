
using EventBooking.Models;

namespace EventBooking.Repo
{
    public interface IEventRepo
    {

        Task CreateEvent(Event data);

        Task<IEnumerable<Event>> GetAllEvents();



    }
}