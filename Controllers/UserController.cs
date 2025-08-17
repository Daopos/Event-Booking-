using System.Threading.Tasks;
using AutoMapper;
using EventBooking.Dtos;
using EventBooking.Repo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventBooking.Controllers
{
    public class UserController : Controller
    {

        private readonly ILogger<UserController> _logger;

        private readonly IEventRepo _eventRepo;

        private readonly IMapper _mapper;


        public UserController(ILogger<UserController> logger, IEventRepo eventRepo, IMapper mapper)
        {
            _logger = logger;
            _eventRepo = eventRepo;
            _mapper = mapper;

        }



        [Authorize(Roles = "User")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var events = _mapper.Map<List<ReadEventDto>>(await _eventRepo.GetAllEvents());

            return View(events);
        }

    }
}