using TaskManager.Models.Enums;

namespace TaskManager.Models.Entities
{
    public class TodoTask : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }

        public TaskPriority Priority { get; set; } = TaskPriority.Medium;
        public StatusTask Status { get; set; } = StatusTask.Pending;

        public int OrderIndex { get; set; }

        public int UserId { get; set; }
        public User? User { get; set; }

        public int? CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
