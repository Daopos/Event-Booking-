
using System.Threading.Tasks;
using AutoMapper;
using EventBooking.Dtos;
using EventBooking.Models;
using EventBooking.Repo;
using EventBooking.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventBooking.Controllers
{
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly IMapper _mapper;
        private readonly IEventRepo _eventRepo;

        public AdminController(ILogger<AdminController> logger, IMapper mapper, IEventRepo eventRepo)
        {
            _logger = logger;
            _mapper = mapper;
            _eventRepo = eventRepo;
        }


        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {


            var events = await _eventRepo.GetAllEvents();

            var eventDtos = _mapper.Map<List<ReadEventDto>>(events);

            var vm = new AdminEventViewModel
            {
                Events = eventDtos,
                CreateEvent = new CreateEventDto() // ensures blank form every time
            };


            return View("Views/Admin/Index.cshtml", vm);
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index(AdminEventViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                TempData["error"] = "Validation failed!";

                return RedirectToAction("Index"); // 🔹 Redirect instead of returning View
            }

            try
            {
                var newEvent = _mapper.Map<Event>(vm.CreateEvent);

                await _eventRepo.CreateEvent(newEvent);

                TempData["success"] = "Event created successfully!";

                return RedirectToAction("Index", "Admin"); // 🔹 PRG pattern
            }
            catch (Exception err)
            {
                TempData["error"] = "Something went wrong while creating the event.";

                return RedirectToAction("Index"); // 🔹 Redirect
            }
        }


    }
}