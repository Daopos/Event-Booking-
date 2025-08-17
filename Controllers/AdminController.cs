
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
            if (vm.CreateEvent == null)
            {
                TempData["error"] = "Can't Create Event!";

                vm.Events = _mapper.Map<List<ReadEventDto>>(await _eventRepo.GetAllEvents());

                // tell the view to reopen modal
                TempData["showModal"] = true;

                return View("Index", vm);
            }

            try
            {
                var newEvent = _mapper.Map<Event>(vm.CreateEvent);

                await _eventRepo.CreateEvent(newEvent);

                TempData["success"] = "Event created successfully!";

                // ✅ Success → redirect (PRG pattern)
                return RedirectToAction("Index", "Admin");
            }
            catch (Exception err)
            {
                TempData["error"] = "Something went wrong!";
                return RedirectToAction("Index", "Admin");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {

            try
            {
                await _eventRepo.DeleteEvent(id);
                TempData["success"] = "Event successfully deleted!";
            }
            catch (Exception err)
            {
                TempData["error"] = "Failed to Delete Event!";

            }

            return RedirectToAction("Index", "Admin");

        }

        [HttpPost]
        public async Task<IActionResult> Edit(AdminEventViewModel vm)
        {
            // if (!ModelState.IsValid)
            // {
            //     TempData["Error"] = "Can't update event";

            //     vm.Events = _mapper.Map<List<ReadEventDto>>(await _eventRepo.GetAllEvents());

            //     // tell the view to reopen modal
            //     TempData["showModal"] = true;

            //     return View("Index", vm);
            // }
            Console.WriteLine($"Id: {vm.UpdateEvent.Id}");
            Console.WriteLine($"Name: {vm.UpdateEvent.EventName}");
            Console.WriteLine($"Date: {vm.UpdateEvent.Date}");
            Console.WriteLine($"Seats: {vm.UpdateEvent.Seats}");
            Console.WriteLine($"Status: {vm.UpdateEvent.Status}");
            try
            {
                var updateEvent = _mapper.Map<Event>(vm.UpdateEvent);

                await _eventRepo.UpdateEvent(updateEvent);

                TempData["Success"] = "Successfully Edited";

                return RedirectToAction("Index", "Admin");

            }
            catch (Exception err)
            {
                TempData["error"] = "Something went wrong!";
                return RedirectToAction("Index", "Admin");
            }
        }

    }
}