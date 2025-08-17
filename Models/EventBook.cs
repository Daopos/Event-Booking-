
using System.ComponentModel.DataAnnotations;

namespace EventBooking.Models
{
    public class EventBook
    {
        [Key]
        public int Id { get; set; }

        public DateTime BookDate { get; set; }

        [Required]
        public int EventId { get; set; }
        public Event Event { get; set; }

        [Required]
        public int userId { get; set; }
        public User User { get; set; }

    }
}