
using System.ComponentModel.DataAnnotations;

namespace EventBooking.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }
        public string EventName { get; set; }

        public DateOnly Date { get; set; }

        public int Seats { get; set; }
        public StatusEnum Status { get; set; }

        public ICollection<EventBook> EventBookings { get; set; }

        public string? Description { get; set; }
        public string? ImageLink { get; set; }


    }

    public enum StatusEnum
    {
        Open,
        Close
    }
}

