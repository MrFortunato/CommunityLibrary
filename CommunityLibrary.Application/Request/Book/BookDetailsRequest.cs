namespace CommunityLibrary.Application.Request
{
    public class BookDetailsRequest
    {
        public Guid Id { get; set; }    
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string AuthorName { get; set; } = string.Empty;
        public string BookCategory { get; set; } = string.Empty;
        public DateTime PublishedDate { get; set; }
    }
}
