
using System.ComponentModel.DataAnnotations;

namespace EventBooking.Dtos
{
    public class CreateEventBookDto
    {

        [Required]
        public int EventId { get; set; }


    }
}