using System.ComponentModel.DataAnnotations;

namespace CommunityLibrary.Application.Request.Rental
{
    public class BookRentalCreateRequest
    {
        [Required(ErrorMessage = "The return date is required.")]
        [DataType(DataType.Date, ErrorMessage = "The return date format is invalid.")]
        public DateTime? ReturnDate { get; set; }

        [Required(ErrorMessage = "The returned status is required.")]
        public bool Returned { get; set; }

        [Required(ErrorMessage = "The book ID is required.")]
        public Guid BookId { get; set; }

        [Required(ErrorMessage = "The registered by user ID is required.")]
        public Guid RegisteredByUserId { get; set; }

        [Required(ErrorMessage = "The client ID is required.")]
        public Guid ClientId { get; set; }

    }
}
