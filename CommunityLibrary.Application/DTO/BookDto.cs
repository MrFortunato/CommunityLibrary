
using System.ComponentModel.DataAnnotations;

namespace CommunityLibrary.Application.DTO
{
    public class BookDto    
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The title is required.")]
        [StringLength(100, ErrorMessage = "The title cannot exceed 100 characters.")]
        public string Title { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "The description cannot exceed 500 characters.")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "An author is required.")]
        public AuthorDto Author { get; set; }

        [Required(ErrorMessage = "The author ID is required.")]
        public Guid AuthorId { get; set; }

        [Required(ErrorMessage = "A book category is required.")]
        public BookCategoryDto BookCategory { get; set; }

        [Required(ErrorMessage = "The book category ID is required.")]
        public Guid BookCategoryId { get; set; }

        [Required(ErrorMessage = "The published date is required.")]
        public DateTime PublishedDate { get; private set; }

        public BookDto() 
        {
            BookCategory = new BookCategoryDto();
            Author = new AuthorDto();   
        }
        public void SetPublishedDate(DateTime publishedDate)
        {
            if (publishedDate > DateTime.UtcNow)
            {
                throw new ArgumentException("The published date cannot be in the future.", nameof(publishedDate));
            }
            PublishedDate = publishedDate;
        }
    }
}
