using Microsoft.AspNetCore.Mvc;
using TaskManager.DTOs;
using TaskManager.Interfaces.Services;
using TaskManager.Models.Entities;

namespace TaskManager.Controllers

{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        public AuthController(IUserService userService, ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        [HttpPost("register")]

        public async Task<ActionResult<UserResponseDTO>> RegisterAsync([FromBody] CreateUserDTO createUserDTO)
        {
            try
            {
                var newUser = new User
                {
                    Name = createUserDTO.Name,
                    Email = createUserDTO.Email,
                };

                await _userService.RegisterAsync(newUser, createUserDTO.Password);

                var userResponse = new UserResponseDTO
                {
                    Id = newUser.Id,
                    Name = newUser.Name,
                    Email = newUser.Email,
                };

                return CreatedAtAction(nameof(GetUserById), new { id = newUser.Id }, userResponse);
            }

            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }

        [HttpGet("{id}")]

        public async Task<ActionResult<UserResponseDTO>> GetUserById(int id)
        {
            try
            {
                var user = await _userService.GetUserProfileAsync(id);

                if (user == null)
                {
                    return NotFound(new { message = "Usuário não encontrado." });
                }

                var userResponse = new UserResponseDTO
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                };

                return Ok(userResponse);
            }

            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("login")]

        public async Task<ActionResult> LoginAsync([FromBody] LoginDTO loginDTO)
        {
            try
            {
                var userLogin = await _userService.ValidateCredentialsAsync(loginDTO.Email, loginDTO.Password);

                if (userLogin == null)
                {
                    return Unauthorized("Credenciais inválidas.");
                }

                var token = _tokenService.CreateToken(userLogin);

                return Ok(new { token = token });
            }

            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
