using System.ComponentModel.DataAnnotations;

namespace CommunityLibrary.Application.Request
{
    public class UserAuthRequest
    {
        [Required(ErrorMessage = "The email is required.")]
        [EmailAddress(ErrorMessage = "The email address is not valid.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "The password is required.")]
        [MinLength(6, ErrorMessage = "The password must be at least 6 characters long.")]
        public string Password { get; set; } = string.Empty;
    }
}
