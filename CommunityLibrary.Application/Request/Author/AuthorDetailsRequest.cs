using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityLibrary.Application.Request
{
    public class AuthorDetailsRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool Status { get; set; }
        public Guid RegisteredByUserId { get; set; }
    }
}
