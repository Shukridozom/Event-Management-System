using AutoMapper;
using EventManagementSystem.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EventParticipationController : AppControllerBase
    {
        public EventParticipationController(AppDbContext context, IConfiguration config, IMapper mapper)
            : base(context, config, mapper)
        {

        }

        [HttpGet]
        [Route("/api/events")]
        public IActionResult GetEvents()
        {
            var events = context.Events.ToList();
            var eventDtos = new List<EventDto>();

            if (events == null)
                return NotFound();

            foreach(var _event in events)
                eventDtos.Add(mapper.Map<Models.Event, EventDto>(_event));

            return Ok(eventDtos);
        }

        [HttpGet]
        [Route("/api/events/{id}")]
        public IActionResult GetEvent(int id)
        {
            var _event = context.Events.SingleOrDefault(e => e.Id == id);
            if (_event == null)
                return NotFound();

            return Ok(mapper.Map<Models.Event, EventDto>(_event));
        }
    }
}
