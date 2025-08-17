using EventBooking.Dtos;

namespace EventBooking.ViewModels
{
    public class AdminEventViewModel
    {
        public CreateEventDto CreateEvent { get; set; } = new CreateEventDto();

        public List<ReadEventDto> Events { get; set; } = new List<ReadEventDto>();

        public UpdateEventDto UpdateEvent { get; set; } = new UpdateEventDto();
    }
}