using CommunityLibrary.Domain.Exceptions;

namespace CommunityLibrary.Domain
{
    public class Client: Entity 
    {
        internal Client(string name)
        {
            ValidateName(name); 
            Id = Guid.NewGuid(); 
            Name = name;
            CreatedDate = DateTime.Now;
            Status = true;
        }
        public void Create(string name)
        {
            ValidateName(name);
            Id = Guid.NewGuid();
            Name = name;
            CreatedDate = DateTime.Now;
            Status = true;
        }
        public void Update(string name)
        {
            ValidateName(name);
            Name = name;
            LastModifiedDate = DateTime.Now;
        }
        private void ValidateName(string name)
        {
            EntityValidationException.Validate(string.IsNullOrEmpty(name), "The name cannot be empty.");
        }
    }
}
