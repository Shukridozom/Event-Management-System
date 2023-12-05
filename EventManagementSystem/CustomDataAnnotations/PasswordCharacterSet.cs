using System.ComponentModel.DataAnnotations;

namespace EventManagementSystem.CustomDataAnnotations
{
    public class PasswordCharacterSet : ValidationAttribute
    {
        string passwordCharacterSet = @"abcdefghijklmnopqrstuvwxyzABCEDFGHIJKLMNOPQRSTUVWXYZ0123456789~`!@#$%^&*()_-+={[}]|\:;""'<,>.?/";
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var password = value?.ToString() ?? string.Empty;

            foreach (var character in password)
            {
                if (!passwordCharacterSet.Contains(character))
                    return new ValidationResult(@"Allowed characters: [a...z][A...Z][0...9] and these symbols: ~`!@#$%^&*()_-+={[}]|\:;""'<,>.?/");
            }
            return ValidationResult.Success;
        }
    }
}
