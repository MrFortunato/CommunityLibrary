using System.ComponentModel.DataAnnotations;

namespace CommunityLibrary.Application.DTO
{
    public class UserDto
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The email is required.")]
        [StringLength(250, ErrorMessage = "The email cannot exceed 250 characters.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "The name is required.")]
        [StringLength(50, ErrorMessage = "The name cannot exceed 50 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "The password is required.")]
        [StringLength(10, MinimumLength = 6, ErrorMessage = "The password must be between 6 and 10 characters.")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "The confirmed password is required.")]
        [StringLength(10, MinimumLength = 6, ErrorMessage = "The confirmed password must be between 6 and 10 characters.")]
        public string ConfirmedPassword { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? LastModifiedDate { get; set; } = null;
        public bool Status { get; set; } = true;
    }
}
