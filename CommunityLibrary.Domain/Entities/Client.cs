using CommunityLibrary.Domain.Exceptions;

namespace CommunityLibrary.Domain
{
    public class Client: Entity 
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid RegisteredByUserId { get; set; }
        public User RegisteredUser { get; set; }
        public ICollection<BookRental> BookRentals { get; set; }
        public Client()
        {
            User = new();
            RegisteredUser = new(); 
            BookRentals = [];
        }   
        internal Client(string name, Guid registeredByUserId, Guid userId) : this()
        {

            ValidateName(name); 
            Id = Guid.NewGuid(); 
            Name = name;
            CreatedDate = DateTime.Now;
            Status = true;
            RegisteredByUserId = registeredByUserId;
            UserId = userId;
        }
        public void Create(string name, Guid userId, Guid registeredUserId)
        {
            ValidateName(name);
            Id = Guid.NewGuid();    
            UserId = userId;
            RegisteredByUserId = registeredUserId;  
            Name = name;
            CreatedDate = DateTime.Now;
            Status = true;
        }
        public void Update(string name,Guid userId)
        {
            ValidateName(name);
            UserId = userId;
            Name = name;
            LastModifiedDate = DateTime.Now;
        }
        private void ValidateName(string name)
        {
            EntityValidationException.Validate(string.IsNullOrEmpty(name), "The name cannot be empty.");
        }
    }
}
