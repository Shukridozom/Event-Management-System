﻿using System.ComponentModel.DataAnnotations;

namespace EventManagementSystem.Dtos
{
    public class EventDto
    {
        public int Id { get; set; }

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
        public int AvailableTickets { get; set; }

        public int UserId { get; set; }

    }
}
