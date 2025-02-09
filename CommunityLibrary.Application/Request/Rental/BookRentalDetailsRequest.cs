namespace CommunityLibrary.Application.Request
{
    public class BookRentalDetailsRequest
    {
        public DateTime CreateDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public bool Returned { get; set; }
        public string BookTitle { get; set; } = string.Empty;
        public string RegisteredByUserName { get; set; }= string.Empty;
        public string ClientName { get; set; } = string.Empty;
    }
}
