using AutoMapper;
using EventManagementSystem.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EventManagementController : AppControllerBase
    {
        public EventManagementController(AppDbContext context, IConfiguration config, IMapper mapper)
            : base(context, config, mapper)
        {

        }

        [HttpPut]
        [Route("/api/events/{id}")]
        public IActionResult EditEvent(int id, CreateEventDto eventDto)
        {
            var eventFromDb = context.Events.SingleOrDefault(e => e.Id == id);
            var userId = GetUserId();

            if (eventFromDb == null)
                return NotFound();

            if (eventFromDb.UserId != userId)
                return NotFound();

            mapper.Map(eventDto, eventFromDb);

            context.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        [Route("/api/events/{id}")]
        public IActionResult DeleteEvent(int id)
        {
            var userId = GetUserId();
            var eventFromDb = context.Events.SingleOrDefault(e => e.Id == id);
            if (eventFromDb == null)
                return NotFound();

            if (eventFromDb.UserId != userId)
                return NotFound();

            context.Events.Remove(eventFromDb);
            context.SaveChanges();

            return Ok();
        }

    }
}
