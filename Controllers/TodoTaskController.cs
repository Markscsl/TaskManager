using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using TaskManager.DTOs.TodoTask;
using TaskManager.Interfaces.Services;
using TaskManager.Models.Entities;
using TaskManager.Models.Enums;

namespace TaskManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoTaskController : ControllerBase
    {
        private readonly ITodoTaskService _todoTaskService;

        public TodoTaskController(ITodoTaskService todoTaskService)
        {
            _todoTaskService = todoTaskService;
        }

        [Authorize]
        [HttpPost]

        public async Task<ActionResult<TodoTaskResponseDTO>> CreateTaskAsync([FromBody] CreateTodoTaskDTO taskDto)
        {
            try
            {
                var newTask = new TodoTask
                {
                    Title = taskDto.Title,
                    Description = taskDto.Description,
                    DueDate = taskDto.DueDate,
                    Priority = taskDto.Priority,
                    Status = taskDto.Status
                };

                var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

                await _todoTaskService.CreateTaskAsync(newTask, userId);

                var response = new TodoTaskResponseDTO
                {
                    Id = newTask.Id,
                    Title = newTask.Title,
                    Description = newTask.Description,
                    DueDate = newTask.DueDate,
                    Priority = newTask.Priority,
                    Status = newTask.Status
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

        public async Task<ActionResult<IEnumerable<TodoTaskResponseDTO>>> GetAllTaskAsync()
        {
            try
            {
                var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

                var tasks = await _todoTaskService.GetUserTaskAsync(userId);

                var response = tasks.Select(t => new TodoTaskResponseDTO
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    DueDate = t.DueDate,
                    Priority = t.Priority,
                    Status = t.Status
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

        public async Task<ActionResult<TodoTaskResponseDTO>> GetTaskByIdAsync(int id)
        {
            try
            {
                var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

                var task = await _todoTaskService.GetTaskByIdAsync(userId, id);

                if (task == null)
                {
                    return NotFound("Nenhuma tarefa encontrada.");
                }

                var response = new TodoTaskResponseDTO
                {
                    Id = task.Id,
                    Title = task.Title,
                    Description = task.Description,
                    DueDate = task.DueDate,
                    Status = task.Status,
                    Priority = task.Priority
                };

                return Ok(response);
            }

            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpPatch("{id}/status")]

        public async Task<ActionResult<TodoTaskResponseDTO>> UpdateTaskStatusAsync(int id, [FromBody] StatusTask novoStatus)
        {
            try
            {
                var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

                var task = await _todoTaskService.UpdateTaskStatusAsync(userId, id, novoStatus);

                if (task == null)
                {
                    return NotFound("Nenhuma tarefa encontrada.");
                }

                var response = new TodoTaskResponseDTO
                {
                    Id = task.Id,
                    Title = task.Title,
                    Description = task.Description,
                    DueDate = task.DueDate,
                    Priority = task.Priority,
                    Status = novoStatus
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

        public async Task<ActionResult<TodoTaskResponseDTO>> UpdateTaskAsync(int id, [FromBody] UpdateTodoTaskDTO updateTodoTaskDTO)
        {
            try
            {
                var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

                var todoTask = new TodoTask
                {
                    Title = updateTodoTaskDTO.Title,
                    Description = updateTodoTaskDTO.Description,
                    DueDate = updateTodoTaskDTO.DueDate,
                    Priority = updateTodoTaskDTO.Priority,
                    Status = updateTodoTaskDTO.Status,
                };

                var taskNova = await _todoTaskService.UpdateTaskAsync(userId, id, todoTask);

                if (taskNova == null)
                {
                    return NotFound("Não encontrado.");
                }

                var response = new TodoTaskResponseDTO
                {
                    Id = taskNova.Id,
                    Title = taskNova.Title,
                    Description = taskNova.Description,
                    DueDate = taskNova.DueDate,
                    Priority = taskNova.Priority,
                    Status = taskNova.Status,
                    UpdatedAt = taskNova.UpdatedAt
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

        public async Task<ActionResult> DeleteTaskAsync(int id)
        {
            try
            {
                var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

                var sucesso = await _todoTaskService.DeleteTaskAsync(userId, id);

                if (!sucesso)
                {
                    return NotFound(new { message = "Nenhuma tarefa encontrada!" });
                }

                return NoContent();
            }

            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpPatch("reorder")]

        public async Task<ActionResult> UpdateTaskOrderAsync([FromBody] List<UpdateTodoTaskOrderDTO> newOrders)
        {
            try
            {
                var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var positionsDict = newOrders.ToDictionary(dto => dto.Id, dto => dto.OrderIndex);

                await _todoTaskService.UpdateTaskOrderAsync(userId, positionsDict);
                return NoContent();
            }

            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
