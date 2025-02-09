using System.Text.Json.Serialization;

namespace CommunityLibrary.Application.Request
{
    public class UserAuthDetailsRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        [JsonIgnore]
        public string Password { get; set; } = string.Empty;
        public DateTime? LastModifiedDate { get; set; } = null;
        public bool Status { get; set; }
        public string Token { get; set; } = string.Empty;   
    }
}
