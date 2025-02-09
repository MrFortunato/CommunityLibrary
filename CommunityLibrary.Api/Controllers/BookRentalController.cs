using CommunityLibrary.Application;
using CommunityLibrary.Application.Interfaces;
using CommunityLibrary.Application.Request;
using CommunityLibrary.Application.Request.Rental;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace CommunityLibrary.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookRentalController : ControllerBase
    {
        private readonly IBookRentalService _bookRentalService;
        public BookRentalController(IBookRentalService bookRentalService)
        {
            _bookRentalService = bookRentalService; 
        }
        [HttpGet("GetAll")]
        public async Task<PaginatedResultService<BookRentalDetailsRequest>> Get([FromQuery] string? filter = null,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            CancellationToken cancellationToken = default)
        {
            Expression<Func<BookRentalDetailsRequest, bool>>? predicate = null;

            if (!string.IsNullOrWhiteSpace(filter))
            {
                predicate = bookRental =>
                    bookRental.BookTitle.Contains(filter) ||
                    bookRental.ClientName.Contains(filter) ||
                    bookRental.RegisteredByUserName.Contains(filter) ||
                    bookRental.CreateDate.Equals(filter);
            }

            var bookRental = await _bookRentalService.GetAllAsync(predicate, pageNumber, pageSize, cancellationToken);
            return bookRental;
        }
        [HttpPost("Create")]
        public async Task<IActionResult> Post([FromBody] BookRentalCreateRequest request)
        {
            if (request == null) 
            {
                BadRequest("Invalid Book Rental Data");
            }
            var bookRental = await _bookRentalService.InsertAsync(request);
            return Ok(new { Message = "BookRental created successfully.", BookRental = bookRental });
        }
    }
}
