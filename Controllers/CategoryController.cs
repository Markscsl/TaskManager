using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskManager.DTOs.Category;
using TaskManager.Interfaces.Services;
using TaskManager.Models.Entities;

namespace TaskManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [Authorize]
        [HttpPost]

        public async Task<ActionResult<CategoryResponseDTO>> CreateCategoryAsync([FromBody] CreateCategoryDTO categoryCreate)
        {
            try
            {
                var newCategory = new Category
                {
                    Name = categoryCreate.Name,
                    ColorHex = categoryCreate.HexColor
                };

                var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

                await _categoryService.CreateCategoryAsync(newCategory, userId);

                var response = new CategoryResponseDTO
                {
                    Id = newCategory.Id,
                    Name = newCategory.Name,
                    ColorHex = newCategory.ColorHex
                };

                return StatusCode(201, response);
            }

            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpGet]

        public async Task<ActionResult<IEnumerable<CategoryResponseDTO>>> GetAllAsync()
        {
            try
            {
                var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var categories = await _categoryService.GetUserCategoriesAsync(userId);

                var response = categories.Select(c => new CategoryResponseDTO
                {
                    Id = c.Id,
                    Name = c.Name,
                    ColorHex = c.ColorHex
                });

                return Ok(response);
            }

            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpGet("{id}")]

        public async Task<ActionResult<CategoryResponseDTO>> GetCategoryByIdAsync(int id)
        {
            try
            {
                var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var category = await _categoryService.GetCategoryByIdAsync(userId, id);

                if (category == null)
                {
                    return NotFound("Nenhuma categoria encontrada.");
                }

                var response = new CategoryResponseDTO
                {
                    Id = category.Id,
                    Name = category.Name,
                    ColorHex = category.ColorHex
                };

                return Ok(response);
            }

            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpPatch("{id}/name")]

        public async Task<ActionResult<CategoryResponseDTO>> UpdateCategoryNameAsync(int id, [FromBody] string name)
        {
            try
            {
                var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var category = await _categoryService.UpdateCategoryNameAsync(userId, id, name);

                if (category == null)
                {
                    return NotFound("Nenhuma categoria encontrada.");
                }

                var response = new CategoryResponseDTO
                {
                    Id = category.Id,
                    Name = name,
                    ColorHex = category.ColorHex
                };

                return Ok(response);
            }

            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpPatch("{id}/color")]

        public async Task<ActionResult<CategoryResponseDTO>> UpdateCategoryColorAsync(int id, [FromBody] string color)
        {
            try
            {
                var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var category = await _categoryService.UpdateCategoryColorAsync(userId, id, color);

                if (category == null)
                {
                    return NotFound("Não encontrado.");
                }

                var response = new CategoryResponseDTO
                {
                    Id = category.Id,
                    Name = category.Name,
                    ColorHex = color
                };

                return Ok(response);
            }

            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpPut("{id}")]

        public async Task<ActionResult<CategoryResponseDTO>> UpdateCategoryAsync(int id, [FromBody] UpdateCategoryDTO updateCategoryDto)
        {
            try
            {

                var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

                var category = new Category
                {
                    Name = updateCategoryDto.Name,
                    ColorHex = updateCategoryDto.ColorHex
                };

                var newCategory = await _categoryService.UpdateCategoryAsync(userId, id, category);

                if (newCategory == null)
                {
                    return NotFound("Não encontrado.");
                }

                var response = new CategoryResponseDTO
                {
                    Id = newCategory.Id,
                    Name = newCategory.Name,
                    ColorHex = newCategory.ColorHex
                };

                return Ok(response);
            }

            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpDelete("{id}")]

        public async Task<ActionResult> DeleteCategoryAsync(int id)
        {
            try
            {
                var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var success = await _categoryService.DeleteCategoryAsync(userId, id);

                if (!success)
                {
                    return NotFound("Não encontrado");
                }

                return NoContent();
            }

            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
