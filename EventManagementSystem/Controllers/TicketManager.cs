using AutoMapper;
using EventManagementSystem.Dtos;
using EventManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketManager : AppControllerBase
    {
        public TicketManager(AppDbContext context, IConfiguration config, IMapper mapper)
            : base(context, config, mapper)
        {

        }

        [Authorize]
        [HttpGet("/api/myTickets")]
        public IActionResult Get([FromQuery]PaginationDto pagination)
        {
            var participations = context.Participations
                .Where(p => p.UserId == GetUserId())
                .Skip((pagination.PageIndex - 1) * pagination.PageLength)
                .Take(pagination.PageLength)
                .ToList();

            var numberOfParticipations = context.Participations.Count(p => p.UserId == GetUserId());

            var participationsDto = new List<ParticipationDto>();

            foreach(var participation in participations)
            {
                participationsDto.Add(new ParticipationDto()
                {
                    EventId = participation.EventId,
                    NumberOfTickets = participation.NumOfTicket
                });
            }

            return Ok(PaginatedList(pagination, numberOfParticipations, participationsDto));
        }

        [HttpPost]
        [Route("/api/cancelParticipation/{eventId}")]
        public IActionResult CancelBooking(int eventId)
        {
            var eventFromDb = context.Events.SingleOrDefault(e => e.Id == eventId);
            var participation = context.Participations
                .SingleOrDefault(p => p.UserId == GetUserId() && p.EventId == eventId);

            if (participation == null)
                return NotFound();

            eventFromDb.AvailableTickets += (uint)participation.NumOfTicket;

            context.Participations.Remove(participation);

            context.SaveChanges();

            return Ok();
        }
    }
}
