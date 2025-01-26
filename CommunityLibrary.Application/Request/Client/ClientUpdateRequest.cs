using System.ComponentModel.DataAnnotations;

namespace CommunityLibrary.Application.Request
{
    public class ClientUpdateRequest
    {
        [Required(ErrorMessage = "The id required.")]
        public Guid Id { get; set; }
        public bool status { get; set; }
    }
}
