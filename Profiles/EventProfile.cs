using AutoMapper;
using EventBooking.Dtos;
using EventBooking.Models;

namespace EventBooking.Profiles
{
    public class EventProfile : Profile
    {
        public EventProfile()
        {
            CreateMap<CreateEventDto, Event>();
            CreateMap<Event, ReadEventDto>();
        }
    }
}