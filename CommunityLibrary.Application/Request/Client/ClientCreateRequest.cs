using System.ComponentModel.DataAnnotations;

namespace CommunityLibrary.Application.Request
{
    public class ClientCreateRequest
    {
        [Required(ErrorMessage = "The user id required.")]
        public Guid UserId { get; set; }    
    }
}
