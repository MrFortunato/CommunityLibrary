using CommunityLibrary.Domain.Exceptions;

namespace CommunityLibrary.Domain
{
    public class Author : Entity
    {
        public ICollection<Book> Books { get; set; } = [];  
        public Guid RegisteredByUserId { get; set; }
        public User User { get; set; }


        public void Create(string name,Guid registeredUserId)
        {
            ValidateName(name);
            Id = Guid.NewGuid();
            CreatedDate = DateTime.Now;
            Status = true;
        }
        public void Update(string name)
        {
            LastModifiedDate = DateTime.Now;
        }

        private void ValidateName(string name)
        {
            EntityValidationException.Validate(string.IsNullOrEmpty(name), "The name cannot be empty.");
        }
    }
}
