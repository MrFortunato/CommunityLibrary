using System.ComponentModel.DataAnnotations;

namespace CommunityLibrary.Domain
{
    public abstract class BaseEntity
    {
        [Key]
        public Guid Id { get; set; } 
        public bool Status { get; set; } = true;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? LastModifiedDate { get; set; } 

    }
}
