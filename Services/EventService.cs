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

        public async Task DeleteEvent(int id)
        {
            var existEvent = await _eventContext.Events.Where(e => e.Id == id).FirstOrDefaultAsync();

            if (existEvent is null)
            {
                throw new ArgumentException("Event doesn't exist");
            }

            _eventContext.Events.Remove(existEvent);
            await _eventContext.SaveChangesAsync();

        }

        public async Task UpdateEvent(Event data)
        {
            var existEvent = await _eventContext.Events.FirstOrDefaultAsync(e => e.Id == data.Id);

            if (existEvent is null)
                throw new ArgumentException("Event doesn't exist");

            // Update fields manually
            existEvent.EventName = data.EventName;
            existEvent.Date = data.Date;
            existEvent.Seats = data.Seats;
            existEvent.Status = data.Status;
            existEvent.Description = data.Description;
            existEvent.ImageLink = data.ImageLink;


            await _eventContext.SaveChangesAsync();
        }


    }
}