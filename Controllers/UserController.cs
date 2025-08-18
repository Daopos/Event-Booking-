using System.Security.Claims;
using AutoMapper;
using EventBooking.Dtos;
using EventBooking.Models;
using EventBooking.Repo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventBooking.Controllers
{
    public class UserController : Controller
    {

        private readonly ILogger<UserController> _logger;

        private readonly IEventBookRepo _bookRepo;

        private readonly IEventRepo _eventRepo;

        private readonly IMapper _mapper;


        public UserController(ILogger<UserController> logger, IEventBookRepo bookRepo, IMapper mapper, IEventRepo eventRepo)
        {
            _logger = logger;
            _bookRepo = bookRepo;
            _mapper = mapper;
            _eventRepo = eventRepo;

        }



        [Authorize(Roles = "User")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var events = _mapper.Map<List<ReadEventDto>>(await _eventRepo.GetAllEvents());

            return View(events);
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public async Task<IActionResult> Book(CreateEventBookDto model)
        {
            // Get logged-in user ID
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);


            try
            {
                var newBook = _mapper.Map<EventBook>(model);
                newBook.userId = int.Parse(userId!);
                await _bookRepo.Create(newBook);
                TempData["success"] = $"Successfully Booked";

                return RedirectToAction("Index", "User");
            }
            catch (Exception ex)
            {
                TempData["error"] = $"Something went wrong: {ex.Message}";
                return RedirectToAction("Index", "User");

            }


        }

    }
}