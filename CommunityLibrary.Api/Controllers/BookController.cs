using CommunityLibrary.Application.Interfaces;
using CommunityLibrary.Application.Request;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CommunityLibrary.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        // Injeção de dependência do IBookService
        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        // GET: api/Book/GetAll
        [HttpGet("GetAll")]
        public async Task<IEnumerable<BookDetailsRequest>> GetAll([FromQuery] string? filter = null,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            CancellationToken cancellationToken = default)
        {
            Func<BookDetailsRequest, bool>? predicate = null;

            // Se houver filtro, aplicamos uma função de filtragem
            if (!string.IsNullOrWhiteSpace(filter))
            {
                predicate = book =>
                    book.Title.Contains(filter) ||
                    book.AuthorName.Contains(filter) ||
                    book.Id.ToString().Equals(filter);
            }

            var books = await _bookService.GetAllAsync(predicate, pageNumber, pageSize, cancellationToken);
            return books;
        }

        // GET: api/Book/GetById/5
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Invalid ID");
            }

            var book = await _bookService.GetByIdAsync(id);
            return Ok(book);
        }

        // POST: api/Book/Create
        [HttpPost("Create")]
        public async Task<IActionResult> Post([FromBody] BookCreateRequest request)
        {
            if (request == null)
            {
                return BadRequest("Invalid book data");
            }

            var createdBook = await _bookService.InsertAsync(request);
            return Ok(new { Message = "Book created successfully.", Book = createdBook });
        }

        // PUT: api/Book/Update
        [HttpPut("Update")]
        public async Task<IActionResult> Put([FromBody] BookUpdateRequest request)
        {
            if (request == null)
            {
                return BadRequest("Invalid book data");
            }

            var updatedBook = await _bookService.UpdateAsync(request);
            return Ok(new { Message = "Book updated successfully.", Book = updatedBook });
        }

        // DELETE: api/Book/Delete/5
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Invalid ID");
            }

            await _bookService.DeleteAsync(id);
            return Ok(new { Message = "Book deleted successfully." });
        }
    }
}
