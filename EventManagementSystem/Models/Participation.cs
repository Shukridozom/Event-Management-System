namespace EventManagementSystem.Models
{
    public class Participation
    {
        public User User { get; set; }
        public int UserId { get; set; }
        public Event Event { get; set; }
        public int EventId { get; set; }
        public int NumOfTicket { get; set; }
    }
}
