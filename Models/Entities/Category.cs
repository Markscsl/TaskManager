namespace TaskManager.Models.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string ColorHex { get; set; } = "#3B82F6";

        public int UserId { get; set; }
        public User? User { get; set; }
    }
}
