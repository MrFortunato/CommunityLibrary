using CommunityLibrary.Domain;
using System.ComponentModel.DataAnnotations;

namespace CommunityLibrary.Application.DTO
{
    public class BookRentalDto
    {
        [Required(ErrorMessage = "The Id field is required.")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The BookId field is required.")]
        public Guid BookId { get; set; }

        [Required(ErrorMessage = "The UserId field is required.")]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "The ClientId field is required.")]
        public Guid ClientId { get; set; }

        [Required(ErrorMessage = "The RentalDate field is required.")]
        [DataType(DataType.Date, ErrorMessage = "The RentalDate must be a valid date.")]
        public DateTime RentalDate { get; set; }

        [DataType(DataType.Date, ErrorMessage = "The ReturnDate must be a valid date.")]
        public DateTime? ReturnDate { get; set; }

        public bool Returned { get; set; }

        [Required(ErrorMessage = "The Book details are required.")]
        public Book Book { get; set; }

        [Required(ErrorMessage = "The User details are required.")]
        public User User { get; set; }

        [Required(ErrorMessage = "The Client details are required.")]
        public Client Client { get; set; }
    }
}
