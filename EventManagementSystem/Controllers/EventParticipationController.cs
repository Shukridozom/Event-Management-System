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
    public class EventParticipationController : AppControllerBase
    {
        public EventParticipationController(AppDbContext context, IConfiguration config, IMapper mapper)
            : base(context, config, mapper)
        {

        }

        [HttpGet]
        [Route("/api/events")]
        public IActionResult GetEvents([FromQuery]PaginationDto pagination)
        {
            var events = context.Events
                .Skip((pagination.PageIndex - 1) * pagination.PageLength)
                .Take(pagination.PageLength)
                .ToList();

            var eventsCount = context.Events.Count();

            var eventDtos = new List<EventDto>();

            if (events == null)
                return NotFound();

            foreach(var _event in events)
                eventDtos.Add(mapper.Map<Models.Event, EventDto>(_event));

            return Ok(PaginatedList(pagination, eventsCount, eventDtos));
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

        [HttpPost]
        [Route("/api/events/book")]
        public IActionResult BookTickets(ParticipationDto dto)
        {
            var eventFromDb = context.Events.SingleOrDefault(e => e.Id == dto.EventId);
            var userId = GetUserId();
            if (eventFromDb == null)
                return BadRequest(GenerateJsonErrorResponse("eventId", "event was not found"));

            if (dto.NumberOfTickets < 1)
                return BadRequest(GenerateJsonErrorResponse("numberOfTickets", "numberOfTickets must be greater than or equal to 0"));

            if (eventFromDb.AvailableTickets < 1)
                return BadRequest(GenerateJsonErrorResponse("alert", "No tickets are available"));

            if (dto.NumberOfTickets > eventFromDb.AvailableTickets)
                return BadRequest(GenerateJsonErrorResponse("alert", $"The number of available tickets is: {eventFromDb.AvailableTickets}"));

            Participation participation = context.Participations
                .SingleOrDefault(p => p.UserId == userId && p.EventId == dto.EventId);

            if (participation == null)
            {
                participation = new Participation()
                {
                    UserId = userId,
                    EventId = dto.EventId,
                    NumOfTicket = dto.NumberOfTickets,
                };
                context.Participations.Add(participation);
            }
            else
            {
                participation.NumOfTicket += dto.NumberOfTickets;
            }

            eventFromDb.AvailableTickets -= (uint)dto.NumberOfTickets;

            context.SaveChanges();

            return Ok();

        }

        [HttpPost]
        [Route("/api/events/cancelBooking/{id}")]
        public IActionResult CancelBooking(int id)
        {
            var userId = GetUserId();
            var eventFromDb = context.Events.SingleOrDefault(e => e.Id == id);
            var participation = context.Participations
                .SingleOrDefault(p => p.UserId == userId && p.EventId == id);

            if (participation == null)
                return BadRequest(GenerateJsonErrorResponse("alert", "participation was not found"));

            eventFromDb.AvailableTickets += (uint)participation.NumOfTicket;

            context.Participations.Remove(participation);

            context.SaveChanges();

            return Ok();

        }
    }
}
