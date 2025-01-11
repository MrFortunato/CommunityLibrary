using CommunityLibrary.Application.DTO;
using CommunityLibrary.Application.Interfaces;
using CommunityLibrary.Application.Request;
using Microsoft.AspNetCore.Mvc;


namespace CommunityLibrary.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userRepository)
        {
            _userService = userRepository;
        }
        [HttpGet("GetAll")]
        public async Task<IEnumerable<UserDetailsRequest>> GetAll([FromQuery] string? filter = null,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            CancellationToken cancellationToken = default)
        {
            Func<UserDetailsRequest, bool>? predicate = null;

            if (!string.IsNullOrWhiteSpace(filter))
            {
                predicate = user =>
                  user.Name.Contains(filter) ||
                  user.Id.ToString().Equals(filter); // Converte Id para string
            }

            var users = await _userService.GetAllAsync(predicate, pageNumber, pageSize, cancellationToken);
            return users;
        }

        // GET api/<UserController>/5
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Invalid ID");
            }
            var user = await _userService.GetByIdAsync(id);
            return Ok(user); 
        }

        // POST api/<UserController>
        [HttpPost("Create")]
        public async Task<IActionResult> Post([FromBody] UserCreateRequest request)
        {
            if (request == null)
            {
                return BadRequest("Invalid user");
            }
            await _userService.InsertAsync(request);
            return Ok(new { Message = "User created successfully.", User = request });
        }

        // PUT api/<UserController>/5
        [HttpPut("Update")]
        public async Task<IActionResult> Put([FromBody] UserUpdateRequest request)
        {
            if (request == null)
            {
                return BadRequest("Invalid user");
            }
            await _userService.UpdateAsync(request);
            return Ok(new { Message = "User edited successfully.", User = request });
        }

        // DELETE api/<UserController>/5
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Invalid ID");
            }
            var deletedUser = await _userService.DeleteAsync(id);
            return Ok(new { Message = "User deleted successfully."});
        }
    }
}
