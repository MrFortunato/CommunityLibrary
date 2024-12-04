using CommunityLibrary.Application.DTO;
using CommunityLibrary.Application.Interfaces;
using CommunityLibrary.Domain;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;


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
            // Construir a expressão de filtro com base no parâmetro fornecido
            Expression<Func<UserDto, bool>>? predicate = null;

            if (!string.IsNullOrWhiteSpace(filter))
            {
                predicate = user => user.Email.Contains(filter);
            }

            var users = await _userService.GetAllAsync(predicate, pageNumber, pageSize, cancellationToken);
            return (IEnumerable<UserDto>)Ok(users);
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await _userService.GetByIdAsync(id);
            return Ok(user); 
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserDto userDto)
        {
            await _userService.InsertAsync(userDto);
            return CreatedAtAction(nameof(GetById), new { id = userDto.Id }, userDto);
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] UserDto userDto)
        {
            await _userService.UpdateAsync(userDto);
            return CreatedAtAction(nameof(GetById), new { id = userDto.Id }, userDto);
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deletedUser = await _userService.DeleteAsync(id);
            return Ok(new { Message = "User deleted successfully.", User = deletedUser });
        }
    }
}
