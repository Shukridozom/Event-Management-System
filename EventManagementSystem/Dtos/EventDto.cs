using System.ComponentModel.DataAnnotations;

namespace EventManagementSystem.Dtos
{
    public class EventDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Location { get; set; }

        public DateTime Date { get; set; }

        public uint AvailableTickets { get; set; }

        public int UserId { get; set; }

    }
}
