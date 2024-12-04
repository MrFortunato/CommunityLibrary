﻿using CommunityLibrary.Domain.Exceptions;

namespace CommunityLibrary.Domain
{
    public class BookCategory : Entity
    {
        public string Description { get; set; } = string.Empty;
        public Guid RegisteredUserId { get; set; }  
        public User RegisteredUser { get; set; }
        public ICollection<Book> Books { get; set; }
        public BookCategory()
        {
            RegisteredUser = new();
            Books = [];
        }
        internal BookCategory(Guid id,string name ,string description): this()  
        {
            ValidateName(name);
            ValidateDescription(description);
            Id = id;
            Name = name;
            Description = description;
            CreatedDate = DateTime.Now;
            Status = true;
        }
        public void Create(string name, string description)
        {
            ValidateName(name);
            ValidateDescription(description);
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            CreatedDate = DateTime.Now;
            Status = true;
        }
        public void Update(string name, string description)
        {
            ValidateName(name);
            ValidateDescription(description);
            Name = name;
            Description = description;
            LastModifiedDate = DateTime.Now;
        }
        private void ValidateName(string name)
        {
            EntityValidationException.Validate(string.IsNullOrEmpty(name), "The name cannot be empty.");
        }
        private void ValidateDescription(string description)
        {
            EntityValidationException.Validate(string.IsNullOrWhiteSpace(description), "The description cannot be empty.");
            EntityValidationException.Validate(description.Length > 200, "The description cannot exceed 200 characters.");
        }
    }
}
