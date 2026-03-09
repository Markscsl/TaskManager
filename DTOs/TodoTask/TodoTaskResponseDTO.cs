using TaskManager.Models.Enums;

namespace TaskManager.DTOs.TodoTask
{
    public class TodoTaskResponseDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DueDate { get; set; }
        public TaskPriority Priority { get; set; }
        public StatusTask Status { get; set; }

    }
}
