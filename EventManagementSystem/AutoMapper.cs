using AutoMapper;
using EventManagementSystem.Dtos;
using EventManagementSystem.Models;

namespace EventManagementSystem
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<RegisterDto, User>();
            CreateMap<LoginDto, User>();
            CreateMap<User, UserDto>();

            CreateMap<CreateEventDto, Event>();
            CreateMap<Event, EventDto>();
        }
    }
}
