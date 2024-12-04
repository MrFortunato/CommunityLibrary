using CommunityLibrary.Domain;

namespace CommunityLibrary
{
    public abstract class Entity:BaseEntity
    {
        public string Name { get; set; } = string.Empty;

    }
}
