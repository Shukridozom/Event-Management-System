using EventManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace EventManagementSystem
{
    public class AppDbContext : DbContext
    {
        private readonly IConfiguration _config;

        public DbSet<User> Users { get; set; }
        public DbSet<Event> Events { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration config)
            : base(options)
        {
            _config = config;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // User Entity:
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<User>()
                .Property(u => u.Username)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(255);

            modelBuilder.Entity<User>()
                .Property(u => u.PasswordHash)
                .IsRequired()
                .HasMaxLength(255);

            modelBuilder.Entity<User>()
                .Property(u => u.FirstName)
                .IsRequired()
                .HasMaxLength(255);

            modelBuilder.Entity<User>()
                .Property(u => u.LastName)
                .IsRequired()
                .HasMaxLength(255);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Events)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId);



            // Event Entity:
            modelBuilder.Entity<Event>()
                .Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255);

            modelBuilder.Entity<Event>()
                .Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(2048);
            
            modelBuilder.Entity<Event>()
                .Property(e => e.Location)
                .IsRequired()
                .HasMaxLength(255);

            modelBuilder.Entity<Event>()
                .Property(e => e.Date)
                .IsRequired();

            modelBuilder.Entity<Event>()
                .Property(e => e.AvailableTickets)
                .IsRequired();


            base.OnModelCreating(modelBuilder);
        }
    }
}
