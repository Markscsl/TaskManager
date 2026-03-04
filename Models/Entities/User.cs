using System.Text.Json.Serialization;

namespace TaskManager.Models.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        [JsonIgnore]
        public string PasswordHash { get; set; } = string.Empty;

        public ICollection<TodoTask> Tasks { get; set; } = new List<TodoTask>();
        public ICollection<Category> Categories { get; set; } = new List<Category>();
    }
}
