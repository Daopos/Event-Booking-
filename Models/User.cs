using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace EventBooking.Models
{
    public class User : IdentityUser<int>
    {

        public string Name { get; set; }

        public RoleEnum Role { get; set; }

        public ICollection<EventBook> EventBookings { get; set; }

    }

    public enum RoleEnum
    {
        Admin,
        User
    }
}