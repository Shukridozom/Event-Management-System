using AutoMapper;
using EventManagementSystem.Dtos;
using EventManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EventManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EventCreationController : AppControllerBase
    {
        public EventCreationController(AppDbContext context, IConfiguration config, IMapper mapper)
            :base(context, config, mapper)
        {

        }

        [HttpPost]
        [Route("/api/events")]
        public IActionResult CreateEvent(CreateEventDto eventDto)
        {
            var userId = GetUserId();
            var newEvent = mapper.Map<CreateEventDto, Models.Event>(eventDto);
            newEvent.UserId = userId;

            context.Events.Add(newEvent);

            context.SaveChanges();

            return Created(Request.GetDisplayUrl() + $"/{newEvent.Id}", mapper.Map<Event, EventDto>(newEvent));
        }
    }
}
