using Microsoft.AspNetCore.Mvc;
using BackendApi.Users.DTOs;
using BackendApi.Users.Services;
using FluentValidation;
namespace BackendApi.Users.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private IValidator<UserInsertDto> _userInsertValidator;

        public UserController(IUserService userService, IValidator<UserInsertDto> userInsertValidator)
        {
            _userService = userService;
            _userInsertValidator = userInsertValidator;
        }

        [HttpGet]
        public async Task<IEnumerable<UserDto>> GetAll()
        {
            var users = await _userService.GetAll();
            return users;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetById(int id)
        {
            var userDto = await _userService.GetById(id);
            return userDto != null ? Ok(userDto) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> Create([FromBody] UserInsertDto userInsertDto)
        {
            var validationResult = await _userInsertValidator.ValidateAsync(userInsertDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            if (!_userService.Validate(userInsertDto))
            {
                return BadRequest(_userService.Errors);
            }
            var userDto = await _userService.Create(userInsertDto);

            return CreatedAtAction(nameof(GetById), new { id = userDto.UserId }, new UserDto
            {
                UserId = userDto.UserId,
                Username = userDto.Username,
                Email = userDto.Email
            });

        }

    }
}
