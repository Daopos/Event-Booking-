using EventBooking.Models;

namespace EventBooking.Dtos
{
    public class ReadEventDto
    {
        public int Id { get; set; }
        public string EventName { get; set; }
        public string Description { get; set; }
        public string ImageLink { get; set; }


        public DateOnly Date { get; set; }

        public int Seats { get; set; }
        public StatusEnum Status { get; set; }
    }
}