namespace CommunityLibrary.Application.Request
{
    public class ClientDetailsRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime? LastModifiedDate { get; set; } = null;
        public bool Status { get; set; }
    }
}
