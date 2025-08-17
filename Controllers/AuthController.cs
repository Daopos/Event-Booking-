

using System.Threading.Tasks;
using EventBooking.Dtos;
using EventBooking.Models;
using EventBooking.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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

                if (user.Role == RoleEnum.Admin)
                {
                    return RedirectToAction("Index", "Admin");

                }
                else if (user.Role == RoleEnum.User)
                {
                    return RedirectToAction("Index", "User");

                }

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
            return View("Views/Auth/Login.cshtml");
        }

        [HttpPost("/login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return View("Login", model);
            }

            var result = await _signInManager.PasswordSignInAsync(
                model.Email,
                model.Password,
                false,
                lockoutOnFailure: false

            );

            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                var roles = await _userManager.GetRolesAsync(user);

                if (roles.Contains("Admin"))
                {
                    return RedirectToAction("Index", "Admin");
                }
                else if (roles.Contains("User"))
                {
                    return RedirectToAction("Index", "User");
                }

            }

            if (result.IsLockedOut)
            {
                ModelState.AddModelError("", "Your account is locked.");
            }
            else
            {
                ModelState.AddModelError("", "Invalid login attempt.");
            }

            return View("Login", model);

        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Auth");
        }



    }
}