using EventManagementSystem.Models;
using System.ComponentModel.DataAnnotations;

namespace EventManagementSystem.Dtos
{
    public class CreateEventDto
    {

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        [MaxLength(2048)]
        public string Description { get; set; }

        [Required]
        [MaxLength(255)]
        public string Location { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [Range(0,int.MaxValue)]
        public int AvailableTickets { get; set; }
    }
}
