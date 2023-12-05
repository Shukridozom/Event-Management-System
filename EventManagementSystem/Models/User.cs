namespace EventManagementSystem.Models
{
    public class User
    {
        public User()
        {
            Events = new List<Event>();
            Participations = new List<Participation>();
        }
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IList<Event> Events { get; set; }
        public IList<Participation> Participations { get; set; }
    }
}
