using CommunityLibrary.Domain.Exceptions;

namespace CommunityLibrary.Domain
{
    public class Author : Entity
    {

        public Guid RegisteredUserId { get; set; }  
        public User RegisteredUser { get; set; }
        public ICollection<Book> Books { get; set; }

        public Author()
        {
            RegisteredUser = new();
            Books = [];

        }
        internal Author( string name, Guid registeredUserId):this()
        {
   
            ValidateName(name);
            Id = Guid.NewGuid();
            RegisteredUserId = registeredUserId;
            Name = name;
            CreatedDate = DateTime.Now;
            Status = true;

        }
        public void Create(string name,Guid registeredUserId)
        {
            ValidateName(name);
            Id = Guid.NewGuid();
            RegisteredUserId = registeredUserId;
            Name = name;
            CreatedDate = DateTime.Now;
            Status = true;
        }
        public void Update(string name)
        {
            Name = name;
            LastModifiedDate = DateTime.Now;
        }

        private void ValidateName(string name)
        {
            EntityValidationException.Validate(string.IsNullOrEmpty(name), "The name cannot be empty.");
        }
    }
}
