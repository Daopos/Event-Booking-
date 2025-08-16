using EventBooking.Models;
using Microsoft.AspNetCore.Identity;

namespace EventBooking.Data
{
    public static class RoleSeeder
    {
        public static async Task SeedRoles(RoleManager<IdentityRole<int>> roleManager)
        {
            foreach (var role in Enum.GetNames(typeof(RoleEnum)))
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole<int>(role));
                }
            }
        }
    }
}