using System;
using System.ComponentModel.DataAnnotations;

namespace CommunityLibrary.Application.Request
{
    public class AuthorCreateRequest
    {

        [Required(ErrorMessage = "The author name is required.")]
        [StringLength(100, ErrorMessage = "The author name must be at most 100 characters long.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "The author status is required.")]
        public bool Status { get; set; }

        [Required(ErrorMessage = "The ID of the user who registered this author is required.")]
        public Guid RegisteredByUserId { get; set; }
    }
}
