using CommunityLibrary.Application;
using CommunityLibrary.Application.Interfaces;
using CommunityLibrary.Application.Request;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace CommunityLibrary.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        // GET: api/Client/GetAll
        [HttpGet("GetAll")]
        public async Task<ActionResult<PaginatedResultService<ClientDetailsRequest>>> GetAll([FromQuery] string? filter = null,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            CancellationToken cancellationToken = default)
        {
            Expression<Func<ClientDetailsRequest, bool>>? predicate = null;

            if (!string.IsNullOrWhiteSpace(filter))
            {
                predicate = client =>
                    client.Name.Contains(filter) ||
                    client.Id.ToString().Equals(filter);
            }

            var clients = await _clientService.GetAllAsync(predicate, pageNumber, pageSize, cancellationToken);
            return clients;
        }

        // GET: api/Client/GetById/5
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Invalid ID");
            }

            var client = await _clientService.GetByIdAsync(id);
            return Ok(client);
        }

        // POST: api/Client/Create
        [HttpPost("Create")]
        public async Task<IActionResult> Post([FromBody] ClientCreateRequest request)
        {
            if (request == null)
            {
                return BadRequest("Invalid client");
            }

            var createdClient = await _clientService.InsertAsync(request);
            return Ok(new { Message = "Client created successfully.", Client = createdClient });
        }

        // PUT: api/Client/Update
        [HttpPut("Update")]
        public async Task<IActionResult> Put([FromBody] ClientUpdateRequest request)
        {
            if (request == null)
            {
                return BadRequest("Invalid client");
            }

            var updatedClient = await _clientService.UpdateAsync(request);
            return Ok(new { Message = "Client updated successfully.", Client = updatedClient });
        }

        // DELETE: api/Client/Delete/5
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Invalid ID");
            }

            await _clientService.DeleteAsync(id);
            return Ok(new { Message = "Client deleted successfully." });
        }
    }
}
