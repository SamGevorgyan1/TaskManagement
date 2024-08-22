using TaskManagement.Domain.Enum;
using TaskStatus = TaskManagement.Domain.Enum.TaskStatus;


namespace TaskManagement.Application.DTOs;

public class TaskDto
{
    public Guid Id { get; set; }
    public string Author { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public TaskStatus Status { get; set; }
    public TaskPriority Priority { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime DueDate { get; set; }
    public List<string> AssigneeEmails { get; set; } = new List<string>();
}