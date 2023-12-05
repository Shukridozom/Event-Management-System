using System.ComponentModel.DataAnnotations;

namespace EventManagementSystem.Dtos
{
    public class ParticipationDto
    {
        [Required]
        public int EventId { get; set; }
        [Required]
        public int NumberOfTickets { get; set; }
    }
}
