using CommunityLibrary.Application;
using CommunityLibrary.Application.Interfaces;
using CommunityLibrary.Application.Request;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace CommunityLibrary.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        // GET: api/Author/GetAll
        [HttpGet("GetAll")]
        public async Task<ActionResult<PaginatedResultService<AuthorDetailsRequest>>> GetAll([FromQuery] string? filter = null,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            CancellationToken cancellationToken = default)
        {
            Expression<Func<AuthorDetailsRequest, bool>>? predicate = null;

            if (!string.IsNullOrWhiteSpace(filter))
            {
                predicate = author =>
                    author.Name.Contains(filter) ||
                    author.Id.ToString().Equals(filter);
            }

            var authors = await _authorService.GetAllAsync(predicate, pageNumber, pageSize, cancellationToken);
            return authors;
        }

        // GET: api/Author/GetById/5
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Invalid ID");
            }

            var author = await _authorService.GetByIdAsync(id);
            return Ok(author);
        }

        // POST: api/Author/Create
        [HttpPost("Create")]
        public async Task<IActionResult> Post([FromBody] AuthorCreateRequest request)
        {
            if (request == null)
            {
                return BadRequest("Invalid author data");
            }

            var createdAuthor = await _authorService.InsertAsync(request);
            return Ok(new { Message = "Author created successfully.", Author = createdAuthor });
        }

        // PUT: api/Author/Update
        [HttpPut("Update")]
        public async Task<IActionResult> Put([FromBody] AuthorUpdateRequest request)
        {
            if (request == null)
            {
                return BadRequest("Invalid author data");
            }

            var updatedAuthor = await _authorService.UpdateAsync(request);
            return Ok(new { Message = "Author updated successfully.", Author = updatedAuthor });
        }

        // DELETE: api/Author/Delete/5
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Invalid ID");
            }

            await _authorService.DeleteAsync(id);
            return Ok(new { Message = "Author deleted successfully." });
        }
    }
}
