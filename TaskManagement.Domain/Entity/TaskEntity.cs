using TaskManagement.Domain.Enum;
using TaskStatus = TaskManagement.Domain.Enum.TaskStatus;

namespace TaskManagement.Domain.Entity;

public class TaskEntity
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public TaskStatus Status { get; set; }
    public TaskPriority Priority { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? DueDate { get; set; }
    public Guid AuthorId { get; set; }
    public User Author { get; set; }
    public ICollection<TaskUser> TaskUsers { get; set; } = [];
}