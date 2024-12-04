using CommunityLibrary.Domain.Exceptions;

namespace CommunityLibrary.Domain
{
    public class Book:BaseEntity
    {
       public string Title { get; private set; } = string.Empty;
       public Guid AuthorId { get; private set; }
       public required Author Author { get; set; }
       public Guid BookCategoryId { get; private set; }
        public required BookCategory BookCategory { get; set; }
       public string Description { get; private set; } = string.Empty;

       public Guid RegisteredByUserId { get; private set; }
       public User RegisteredUser { get; set; }
        public DateTime PublishedDate { get; private set; }
       public ICollection<BookRental> BookRentals { get; set; }

        public Book()
        {
            RegisteredUser = new();
            Author = new();
            BookCategory = new();
            BookRentals = [];
        }
        internal Book(string title, string description, DateTime publishedDate, Author author, BookCategory bookCategory):this()    
        {
          
 
            ValidateTitle(title);
            ValidateDescription(description);
            ValidatePublishedDate(publishedDate);
            ValidateAuthor(author);
            ValidateBookCategory(bookCategory);

            Id = Guid.NewGuid();
            Title = title;
            Description = description;
            PublishedDate = publishedDate;
            AuthorId = author.Id;
            BookCategoryId = bookCategory.Id;
        }
        private void ValidateTitle(string title)
        {
            EntityValidationException.Validate(string.IsNullOrWhiteSpace(title), "The title cannot be empty.");
            EntityValidationException.Validate(title.Length > 100, "The title cannot exceed 100 characters.");
        }

        private void ValidateDescription(string description)
        {
            EntityValidationException.Validate(string.IsNullOrWhiteSpace(description), "The description cannot be empty.");
            EntityValidationException.Validate(description.Length > 200, "The description cannot exceed 200 characters.");
        }

        private void ValidatePublishedDate(DateTime publishedDate)
        {
            EntityValidationException.Validate(publishedDate > DateTime.Now, "The published date cannot be in the future.");
        }

        private void ValidateAuthor(Author author)
        {
            EntityValidationException.Validate(author is null, "The author cannot be null.");
        }

        private void ValidateBookCategory(BookCategory bookCategory)
        {
            EntityValidationException.Validate(bookCategory is null, "The book category cannot be null.");
        }

        public void Create(string title, string description, DateTime publishedDate, Author author, BookCategory bookCategory)
        {
            ValidateTitle(title);
            ValidateDescription(description);
            ValidatePublishedDate(publishedDate);
            ValidateAuthor(author);
            ValidateBookCategory(bookCategory);

            Id = Guid.NewGuid();
            Title = title;
            Description = description;
            PublishedDate = publishedDate;
            AuthorId = author.Id;
            BookCategoryId = bookCategory.Id;
        }   

        public void Update(string title, string description, DateTime publishedDate, Author author, BookCategory bookCategory)
        {
            ValidateTitle(title);
            ValidateDescription(description);
            ValidatePublishedDate(publishedDate);
            ValidateAuthor(author);
            ValidateBookCategory(bookCategory);

            Title = title;
            Description = description;
            PublishedDate = publishedDate;
            AuthorId = author.Id;
            BookCategoryId = bookCategory.Id;
        }
    }
}
