using CommunityLibrary.Application.DTO;
using CommunityLibrary.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace CommunityLibrary.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    
    public class UserController : ControllerBase
    {
        private readonly IGenerecService<UserDto> _userService;
        public UserController(IGenerecService<UserDto> userRepository)
        {
            _userService = userRepository;
        }
        [HttpGet("GetAll")]
        public async Task<IEnumerable<UserDto>> GetAll([FromQuery] string? filter = null,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            CancellationToken cancellationToken = default)
        {
            Func<UserDto, bool>? predicate = null;

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
        public async Task<IActionResult> Post([FromBody] UserDto userDto)
        {
            if (userDto == null)
            {
                return BadRequest("Invalid user");
            }
            await _userService.InsertAsync(userDto);
            return Ok(new { Message = "User created successfully.", User = userDto });
        }

        // PUT api/<UserController>/5
        [HttpPut("Update")]
        public async Task<IActionResult> Put([FromBody] UserDto userDto)
        {
            if (userDto == null)
            {
                return BadRequest("Invalid user");
            }
            await _userService.UpdateAsync(userDto);
            return Ok(new { Message = "User edited successfully.", User = userDto });
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
