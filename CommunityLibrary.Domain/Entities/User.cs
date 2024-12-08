using CommunityLibrary.Domain.Exceptions;

namespace CommunityLibrary.Domain
{
    public class User : Entity
    {
        public string Email { get; private set ; } = string.Empty;
        public string Password { get; private set; } = string.Empty;
        public string ConfirmedPassword { get; private set; } = string.Empty;
        public Client Client { get; set; }
        public ICollection<BookRental> BookRentals { get; set; }
        public ICollection<Book> RegisteredBooks { get; set; }
        public ICollection<BookCategory> RegisteredBookCategories { get; set; }
        public ICollection<Client> RegisteredClients { get; set; }

        public ICollection<Author> RegisteredAuthors { get; set; }
        public User()
        {
            Id = Guid.NewGuid(); 
            LastModifiedDate = null;
            RegisteredBookCategories = [];
            BookRentals = [];
            RegisteredBooks = [];
            RegisteredClients = [];
            RegisteredAuthors = [];
        }
        
        public void Create(string email, string password, string confirmedPassword, string name)
        {
            ValidateEmail(email);
            ValidatePassword(password, confirmedPassword);
            Id = Guid.NewGuid();
            Email = email;
            Password = password;
            Name = name;
            CreatedDate = DateTime.Now;
            Status = true;
        }
        public void Update(string email, string password, string confirmedPassword, string name)
        {
            ValidateEmail(email);
            ValidatePassword(password, confirmedPassword);
            Email = email;
            Password = password;
            Name = name;
            LastModifiedDate = DateTime.Now;
        }
        private void ValidateEmail(string? email)
        {
            email = email?.Trim();
            EntityValidationException.Validate(string.IsNullOrEmpty(email), "The email cannot be empty.");

            const int maxEmailLength = 254;
            EntityValidationException.Validate(email?.Length > maxEmailLength, $"The email cannot exceed {maxEmailLength} characters.");

            const string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            bool isValidFormat = System.Text.RegularExpressions.Regex.IsMatch(email, emailPattern);
            EntityValidationException.Validate(!isValidFormat, "The email format is invalid.");

        }
        public void ValidatePassword(string password, string confirmedPassword)
        {
            password = password?.Trim();
            confirmedPassword = confirmedPassword?.Trim();
            EntityValidationException.Validate(string.IsNullOrEmpty(password), "The password cannot be empty.");
            EntityValidationException.Validate(password.Length < 8, "The password must be at least 8 characters long.");
            EntityValidationException.Validate(password.Length > 20, "The password cannot exceed 20 characters.");
            EntityValidationException.Validate(password != confirmedPassword, "The password and confirmed password do not match.");
        }
        
    }

}
