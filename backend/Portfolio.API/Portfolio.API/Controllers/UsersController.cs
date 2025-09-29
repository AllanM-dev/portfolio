using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Application.DTOs.Users;
using Portfolio.Application.UseCases.Experiences;
using Portfolio.Application.UseCases.Users;

namespace Portfolio.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly AddUser _addUser;
        private readonly GetUsers _getUsers;
        private readonly GetUserById _getUserById;

        public UsersController(AddUser addUser, GetUsers getUsers, GetUserById getUserById)
        {
            _addUser = addUser;
            _getUsers = getUsers;
            _getUserById = getUserById;
        }

        [HttpGet]
        public ActionResult<IEnumerable<UserDto>> GetAll()
        {
            var users = _getUsers.Execute();
            return Ok(users);
        }

        [HttpPost]
        [ProducesResponseType<UserDto>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Create([FromBody] CreateUserDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var createdUser = dto.ToEntity();
            _addUser.Execute(createdUser);
            return CreatedAtAction(nameof(GetById), new { id = createdUser.Id }, createdUser.ToDto());
        }

        [HttpGet("{id}")]
        public ActionResult<UserDto> GetById(Guid id)
        {
            var user = _getUserById.Execute(id);
            if (user == null)
                return NotFound();
            return Ok(user.ToDto());
        }
    }
}
