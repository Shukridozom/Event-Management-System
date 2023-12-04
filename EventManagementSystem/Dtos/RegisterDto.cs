using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using EventManagementSystem.CustomDataAnnotations;

namespace EventManagementSystem.Dtos
{
    public class RegisterDto
    {
        [Required]
        [MaxLength(50)]
        [UsernameCharacterSet]
        public string Username { get; set; }

        [Required]
        [MaxLength(255)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(16)]
        [PasswordCharacterSet]
        public string Password { get; set; }

        [Required]
        [MaxLength(16)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Required]
        [MaxLength(255)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(255)]
        public string LastName { get; set; }
    }
}
