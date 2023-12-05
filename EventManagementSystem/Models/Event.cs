namespace EventManagementSystem.Models
{
    public class Event
    {
        public Event()
        {
            Participations = new List<Participation>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public DateTime Date { get; set; }
        public uint AvailableTickets { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public IList<Participation> Participations { get; set; }
    }
}
