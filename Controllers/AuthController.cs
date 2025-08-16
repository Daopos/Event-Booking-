

using EventBooking.Dtos;
using EventBooking.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EventBooking.Controllers
{
    public class AuthController : Controller
    {
        private readonly ILogger<AuthController> _logger;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;


        public AuthController(ILogger<AuthController> logger, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet("/signup")]
        public IActionResult Signup()
        {

            if (User.Identity.IsAuthenticated)
            {

                if (User.IsInRole("Admin"))
                {
                    return RedirectPermanent("/admin/index");
                }
                else
                {
                    return RedirectPermanent("/user/index");


                }

            }


            return View("Views/Auth/Signup.cshtml");
        }

        [HttpPost("/signup")]
        public async Task<IActionResult> Signup(CreateUserDto model)
        {
            if (!ModelState.IsValid)
            {
                return View("Signup", model);
            }

            var user = new User
            {
                UserName = model.Email,
                Email = model.Email,
                Name = model.Name,
                Role = model.Role
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, model.Role.ToString());

                await _signInManager.SignInAsync(user, isPersistent: false);

                return RedirectToAction("Index", "Admin");
            }

            // Add errors from IdentityResult to ModelState
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View("Signup", model);
        }




        [HttpGet("/login")]
        public IActionResult Login()
        {
            return View("Views/Auth/Login.cshtml");
        }



    }
}