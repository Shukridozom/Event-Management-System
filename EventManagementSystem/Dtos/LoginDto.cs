using EventManagementSystem.CustomDataAnnotations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace EventManagementSystem.Dtos
{
    public class LoginDto
    {
        [Required]
        [MaxLength(50)]
        [UsernameCharacterSet]
        public string Username { get; set; }

        [Required]
        [MaxLength(16)]
        [MinLength(6)]
        [PasswordCharacterSet]
        public string Password { get; set; }
    }
}
