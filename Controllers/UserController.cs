using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskManager.DTOs.Auth;
using TaskManager.DTOs.User;
using TaskManager.Interfaces.Services;
using TaskManager.Models.Entities;

namespace TaskManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("me")]
        
        public async Task<ActionResult<UserProfileResponseDTO>> GetMyProfileAsync()
        {
            try
            {
                var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

                var user = await _userService.GetUserProfileAsync(userId);

                if (user == null)
                {
                    return NotFound("Não encontrado.");
                }

                var response = new UserProfileResponseDTO
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                };

                return Ok(response);
            }

            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("me")]

        public async Task<ActionResult> UpdateUserProfileAsync([FromBody] UserProfileUpdateDTO userUpdate)
        {
            try
            {
                var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

                var user = new User
                {
                    Name = userUpdate.Name,
                    Email = userUpdate.Email,
                };

                await _userService.UpdateUserProfileAsync(userId, user);

                return Ok();
            }

            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
