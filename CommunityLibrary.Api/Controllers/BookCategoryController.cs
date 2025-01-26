using CommunityLibrary.Application;
using CommunityLibrary.Application.Interfaces;
using CommunityLibrary.Application.Request;
using Microsoft.AspNetCore.Mvc;

namespace CommunityLibrary.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookCategoryController : ControllerBase
    {
        private readonly IBookCategoryService _bookCategoryService;

        // Injeção de dependência do IBookCategoryService
        public BookCategoryController(IBookCategoryService bookCategoryService)
        {
            _bookCategoryService = bookCategoryService;
        }

        // GET: api/BookCategory/GetAll
        [HttpGet("GetAll")]
        public async Task<PaginatedResultService<BookCategoryDetailsRequest>> GetAll([FromQuery] string? filter = null,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            CancellationToken cancellationToken = default)
        {
            Func<BookCategoryDetailsRequest, bool>? predicate = null;

            // Se houver filtro, aplicamos uma função de filtragem
            if (!string.IsNullOrWhiteSpace(filter))
            {
                predicate = category =>
                    category.Name.Contains(filter) ||
                    category.Id.ToString().Equals(filter);
            }

            var categories = await _bookCategoryService.GetAllAsync(predicate, pageNumber, pageSize, cancellationToken);
            return categories;
        }

        // GET: api/BookCategory/GetById/5
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Invalid ID");
            }

            var category = await _bookCategoryService.GetByIdAsync(id);
            return Ok(category);
        }

        // POST: api/BookCategory/Create
        [HttpPost("Create")]
        public async Task<IActionResult> Post([FromBody] BookCategoryCreateRequest request)
        {
            if (request == null)
            {
                return BadRequest("Invalid category");
            }

            var createdCategory = await _bookCategoryService.InsertAsync(request);
            return Ok(new { Message = "Category created successfully.", Category = createdCategory });
        }

        // PUT: api/BookCategory/Update
        [HttpPut("Update")]
        public async Task<IActionResult> Put([FromBody] BookCategoryUpdateRequest request)
        {
            if (request == null)
            {
                return BadRequest("Invalid category");
            }

            var updatedCategory = await _bookCategoryService.UpdateAsync(request);
            return Ok(new { Message = "Category updated successfully.", Category = updatedCategory });
        }

        // DELETE: api/BookCategory/Delete/5
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Invalid ID");
            }

            await _bookCategoryService.DeleteAsync(id);
            return Ok(new { Message = "Category deleted successfully." });
        }
    }
}
