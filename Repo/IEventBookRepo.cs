


using EventBooking.Models;

namespace EventBooking.Repo
{
    public interface IEventBookRepo
    {
        Task Create(EventBook data);

        IEnumerable<EventBook> GetEventBookByUserId();
    }
}