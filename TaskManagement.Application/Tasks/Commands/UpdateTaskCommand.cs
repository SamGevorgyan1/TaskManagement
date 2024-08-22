using Convey.CQRS.Commands;
using TaskManagement.Domain.Enum;
using TaskStatus = TaskManagement.Domain.Enum.TaskStatus;

namespace TaskManagement.Application.Tasks.Commands;

public class UpdateTaskCommand : ICommand
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public TaskStatus Status { get; set; }
    public TaskPriority Priority { get; set; }
    public DateTime? DueDate { get; set; }
    public List<string>? AssigneeEmails { get; set; }
}