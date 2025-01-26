namespace CommunityLibrary.Domain
{
    public class BookRental: BaseEntity
    {
        public DateTime RentalDate { get; set; }
        public DateTime? ReturnDate { get; set; } 
        public bool Returned { get; set; }
        public Guid BookId { get; set; }
        public Book? Book { get; set; }
        public Guid RegisteredByUserId { get; set; }
        public User? RegisteredByUser { get; set; }
        public Guid ClientId { get; set; }
        public Client? Client { get; set; }
 
    }
}
