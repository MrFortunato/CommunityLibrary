namespace CommunityLibrary.Application.Security
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(string username, IEnumerable<string> roles);
    }
}
