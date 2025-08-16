using EventBooking.Models;

namespace EventBooking.Dtos
{
    public class CreateEventDto
    {
        public string EventName { get; set; }

        public DateOnly? Date { get; set; }

        public int Seats { get; set; }

        public StatusEnum? Status { get; set; }
    }
}