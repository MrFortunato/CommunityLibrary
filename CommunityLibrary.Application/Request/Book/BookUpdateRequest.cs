using System.ComponentModel.DataAnnotations;

namespace CommunityLibrary.Application.Request
{
    public class BookUpdateRequest
    {
        [Required(ErrorMessage = "Book ID is required.")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters.")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Status is required.")]
        public bool Status { get; set; } = true;

        [DataType(DataType.Date, ErrorMessage = "Published date must be a valid date.")]
        [Required(ErrorMessage = "Published date is required.")]
        public DateTime PublishedDate { get; set; }

        [Required(ErrorMessage = "Author ID is required.")]
        public Guid AuthorId { get; set; }

        [Required(ErrorMessage = "BookCategoryId is required.")]
        public Guid BookCategoryId { get; set; }

    }
}
