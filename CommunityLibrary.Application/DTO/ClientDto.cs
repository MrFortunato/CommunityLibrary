using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityLibrary.Application.DTO
{
    public class ClientDto
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The name is required.")]
        [StringLength(50, ErrorMessage = "The name cannot exceed 50 characters.")]
        public string Name { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? LastModifiedDate { get; set; }
        public bool Status { get; set; } = true;
    }
}
