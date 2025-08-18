using AutoMapper;
using EventBooking.Dtos;
using EventBooking.Models;

namespace EventBooking.Profiles
{
    public class EventBookingProfile : Profile
    {

        public EventBookingProfile()
        {
            CreateMap<CreateEventBookDto, EventBook>();
        }

    }
}