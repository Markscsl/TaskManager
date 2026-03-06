using TaskManager.Models.Enums;

namespace TaskManager.DTOs.TodoTask
{
    public class CreateTodoTaskDTO
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime? DueDate { get; set; }
        public TaskPriority Priority { get; set; } = TaskPriority.Low;
        public StatusTask Status { get; set; } = StatusTask.Pending;
    }
}
