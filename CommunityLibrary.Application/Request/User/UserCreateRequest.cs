using System.ComponentModel.DataAnnotations;

namespace CommunityLibrary.Application.Request
{
    public class UserCreateRequest : IValidatableObject
    {
        [Required(ErrorMessage = "The user name is required.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "The user email is required.")]
        [EmailAddress(ErrorMessage = "The email address is not valid.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "The user password is required.")]
        [MinLength(6, ErrorMessage = "The password must be at least 6 characters long.")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "The confirmation password is required.")]
        [Compare(nameof(Password), ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmedPassword { get; set; } = string.Empty;

        // Optional: Additional validation logic
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!string.IsNullOrWhiteSpace(Password) && Password.Length < 6)
            {
                yield return new ValidationResult(
                    "The password must be at least 6 characters long.",
                    new[] { nameof(Password) }
                );
            }
        }
    }
}
