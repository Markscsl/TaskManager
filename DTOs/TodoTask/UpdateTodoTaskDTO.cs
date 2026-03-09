using TaskManager.Models.Enums;

namespace TaskManager.DTOs.TodoTask
{
    public class UpdateTodoTaskDTO
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime? UpdatedAt { get; set; } 
        public DateTime? DueDate { get; set; }
        public TaskPriority Priority { get; set; } = TaskPriority.Medium;
        public StatusTask Status { get; set; } = StatusTask.Pending;
    }
}
