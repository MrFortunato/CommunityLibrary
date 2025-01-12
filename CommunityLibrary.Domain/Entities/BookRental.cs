namespace CommunityLibrary.Domain
{
    public class BookRental: BaseEntity
    {
        public Guid BookId { get; set; } 
        public Guid UserId { get; set; }
        public Guid ClientId { get; set; }
        public DateTime RentalDate { get; set; }
        public DateTime? ReturnDate { get; set; } 
        public bool Returned { get; set; } 
        public Book? Book { get; set; } 
        public User? User { get; set; } 
        public Client? Client { get; set; }
 
    }
}
