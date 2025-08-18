
using EventBooking.Data;
using EventBooking.Models;
using EventBooking.Repo;
using Microsoft.EntityFrameworkCore;

namespace EventBooking.Services
{
    public class EventBookService : IEventBookRepo
    {

        private readonly AppDbContext _eventBookContext;

        public EventBookService(AppDbContext eventBookContext, AppDbContext eventContext)
        {
            _eventBookContext = eventBookContext;
        }

        public async Task Create(EventBook data)
        {

            var existEvent = await _eventBookContext.Events.Where(e => e.Id == data.EventId && e.Status == StatusEnum.Open).FirstOrDefaultAsync();

            if (existEvent is null)
            {
                throw new ArgumentException("Event is close.");

            }

            data.BookDate = DateTime.UtcNow;


            _eventBookContext.EventBooks.Add(data);
            await _eventBookContext.SaveChangesAsync();

        }


        public async Task<IEnumerable<EventBook>> GetEventBookByUserId(int userId)
        {

            var userExists = await _eventBookContext.Users.AnyAsync(u => u.Id == userId);

            if (!userExists)
                throw new ArgumentException("User does not exist.");


            return await _eventBookContext.EventBooks.Where(e => e.userId == userId).ToListAsync();

        }

    }
}