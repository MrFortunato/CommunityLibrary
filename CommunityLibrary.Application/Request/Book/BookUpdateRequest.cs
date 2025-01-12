using System.ComponentModel.DataAnnotations;

namespace CommunityLibrary.Application.Request
{
    public class BookUpdateRequest
    {
        [Required(ErrorMessage = "Book ID is required.")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Category Book ID is required.")]
        public Guid CategoryBookId { get; set; }

        [Required(ErrorMessage = "Author ID is required.")]
        public Guid AuthorId { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        public bool Status { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Published date must be a valid date.")]
        [Required(ErrorMessage = "Published date is required.")]
        public DateTime PublishedDate { get; set; }

        [Required(ErrorMessage = "Registered by user ID is required.")]
        public Guid RegisteredByUserId { get; set; }
    }
}
