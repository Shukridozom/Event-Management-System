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
        [HttpGet]
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
    }
}
