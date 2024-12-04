using System.ComponentModel.DataAnnotations;

namespace CommunityLibrary.Application.DTO
{
    public class AuthorDto
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The name is required.")]
        [StringLength(50, ErrorMessage = "The name cannot exceed 50 characters.")]
        public string Name { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? LastModifiedDate { get; set; }
        public bool Status { get; set; } = true;
    }
}
