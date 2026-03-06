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
                    Name = newCategory.Name
                };

                return StatusCode(201, response);
            }

            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message  });
            }
        }
    }
}
