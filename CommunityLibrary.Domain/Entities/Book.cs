namespace CommunityLibrary.Domain.Entities
{
    public class Book
    {
       public int Id { get; set; }
       public string Title { get; set; } = string.Empty;
       public int AuthorId { get; set; }
       public required Author Author { get; set; }
       public int BookCategoryId { get; set; }
       public required BookCategory BookCategory { get; set; }
       public string Description { get; set; } = string.Empty;
       public DateTime PublishedDate { get; set; }
       public bool IsActive { get; set; }   


    }
}
