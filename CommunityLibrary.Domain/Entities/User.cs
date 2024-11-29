namespace CommunityLibrary.Domain.Entities
{
    public class User : Entity
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool IsActive { get; set; }

    }
}
