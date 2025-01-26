namespace CommunityLibrary.Application.Request
{
    public class BookCategoryDetailsRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool status { get; set; }
        public string RegisteredByUserName { get; set; }  
        
     
    }
}
