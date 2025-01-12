using System.ComponentModel.DataAnnotations;

namespace CommunityLibrary.Application.Request
{
    public class BookCategoryUpdateRequest
    {
        [Required(ErrorMessage = "The Id is required.")]
        public Guid Id { get; set; }    
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(500, ErrorMessage = "Description cannot be longer than 500 characters.")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Status is required.")]
        public bool Status { get; set; }

        [Required(ErrorMessage = "RegisteredByUserId is required.")]
        public Guid RegisteredByUserId { get; set; }
    }
}
