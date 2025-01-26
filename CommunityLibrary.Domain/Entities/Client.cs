using CommunityLibrary.Domain.Exceptions;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommunityLibrary.Domain
{
    public class Client: BaseEntity 
    {
        public ICollection<BookRental> BookRentals { get; set; } = [];
        public Guid UserId { get; set; }
        public User User { get; set; } = new();

        public void Create( Guid userId)
        {
            CreatedDate = DateTime.Now;
            Status = true;
        }
        public void Update(Guid userId)
        {
            LastModifiedDate = DateTime.Now;
        }
        private void ValidateName(string name)
        {
            EntityValidationException.Validate(string.IsNullOrEmpty(name), "The name cannot be empty.");
        }
    }
}
