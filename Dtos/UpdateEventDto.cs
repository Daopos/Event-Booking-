
using System.ComponentModel.DataAnnotations;
using EventBooking.Models;

namespace EventBooking.Dtos
{
    public class UpdateEventDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Event name is required")]
        [StringLength(100, ErrorMessage = "Event name cannot be longer than 100 characters")]
        public string EventName { get; set; }

        [Required(ErrorMessage = "Date is required")]
        public DateOnly? Date { get; set; }

        [Required(ErrorMessage = "Seats are required")]
        [Range(1, 10000, ErrorMessage = "Seats must be between 1 and 10,000")]
        public int Seats { get; set; }

        [Required(ErrorMessage = "Status is required")]
        public StatusEnum? Status { get; set; }


        [StringLength(244, ErrorMessage = "Event description cannot be longer than 244 characters")]
        public string? Description { get; set; }

        [StringLength(244, ErrorMessage = "Image description cannot be longer than 244 characters")]
        public string? ImageLink { get; set; }
    }
}