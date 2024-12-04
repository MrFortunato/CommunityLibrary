namespace CommunityLibrary.Domain
{
    public class BookRental
    {
        public Guid Id { get; set; } 
        public Guid BookId { get; set; } 
        public Guid UserId { get; set; }
        public Guid ClientId { get; set; }
        public DateTime RentalDate { get; set; }
        public DateTime? ReturnDate { get; set; } 
        public bool Returned { get; set; } 
        public Book Book { get; set; } 
        public User User { get; set; } 
        public Client Client { get; set; }

        public BookRental()
        {
            User = new();
            //Book = new();
            Client = new();


        }

        internal BookRental(Guid bookId, Guid userId, Guid clientId) : this()
        {
            Id = Guid.NewGuid();
            BookId = bookId;
            UserId = userId;
            ClientId = clientId;
            RentalDate = DateTime.Now;
            Returned = false;
        }   
    }
}
