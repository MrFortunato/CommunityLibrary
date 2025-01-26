using System.ComponentModel.DataAnnotations;

namespace CommunityLibrary.Application.Request
{
    public class AuthorUpdateRequest
    {
        [Required(ErrorMessage = "The author ID is required.")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The author name is required.")]
        [StringLength(100, ErrorMessage = "The author name must be at most 100 characters long.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "The author status is required.")]
        public bool Status { get; set; }

    }
}
