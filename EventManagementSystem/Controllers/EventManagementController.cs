using AutoMapper;
using EventManagementSystem.Dtos;
using EventManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        [HttpGet]
        [Route("/api/events/participants/{id}")]
        public IActionResult GetEventParticipants(int id)
        {
            var eventFromDb = context.Events.Include(e => e.Participations).SingleOrDefault(e => e.Id == id);
            var userId = GetUserId();
            var participantsDto = new List<EventParticipantsDto>();

            if (eventFromDb == null)
                return NotFound();

            if (eventFromDb.UserId != userId)
                return NotFound();

            foreach(var participation in eventFromDb.Participations)
            {
                participantsDto.Add(new EventParticipantsDto() 
                {
                    UserId = participation.UserId,
                    NumberOfTickets = participation.NumOfTicket
                });
            }

            return Ok(participantsDto);

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
