using System.ComponentModel.DataAnnotations;

namespace EventManagementSystem.CustomDataAnnotations
{
    public class UsernameCharacterSet : ValidationAttribute
    {
        string usernameCharacterSet = "abcdefghijklmnopqrstuvwxyzABCEDFGHIJKLMNOPQRSTUVWXYZ0123456789_";
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            string username = value?.ToString() ?? string.Empty;

            if (username.Length < 6)
                return new ValidationResult("username must be at least 6 characters long");

            foreach (var character in username)
            {
                if (!usernameCharacterSet.Contains(character))
                    return new ValidationResult("Allowed characters: [a...z][A...Z][0...9] and _");
            }
            return ValidationResult.Success;
        }
    }
}
