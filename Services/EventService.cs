using EventBooking.Data;
using EventBooking.Models;
using EventBooking.Repo;
using Microsoft.EntityFrameworkCore;

namespace EventBooking.Services
{
    public class EventService : IEventRepo
    {

        private readonly AppDbContext _eventContext;

        public EventService(AppDbContext eventContext)
        {
            _eventContext = eventContext;
        }

        public async Task CreateEvent(Event data)
        {
            if (data == null)
            {
                throw new ArgumentException(nameof(data));
            }

            try
            {
                _eventContext.Add(data);
                await _eventContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Event>> GetAllEvents()
        {
            return await _eventContext.Events.ToListAsync();
        }



    }
}