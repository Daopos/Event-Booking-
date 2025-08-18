


using EventBooking.Models;

namespace EventBooking.Repo
{
    public interface IEventBookRepo
    {
        Task Create(EventBook data);

        Task<IEnumerable<EventBook>> GetEventBookByUserId(int UserId);
    }
}