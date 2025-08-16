using System.ComponentModel.DataAnnotations;
using EventBooking.Models;

namespace EventBooking.Dtos
{
    public class CreateUserDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        [Required]
        public RoleEnum Role { get; set; }
    }
}