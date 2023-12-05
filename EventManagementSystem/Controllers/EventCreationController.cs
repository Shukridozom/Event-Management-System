using AutoMapper;
using EventManagementSystem.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        public IActionResult Event(NewEventDto eventDto)
        {
            var userId = GetUserId();
            var newEvent = mapper.Map<NewEventDto, Models.Event>(eventDto);
            newEvent.UserId = userId;

            context.Events.Add(newEvent);

            context.SaveChanges();

            return Ok();
        }
    }
}
