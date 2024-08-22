namespace TaskManagement.Domain.Entity;


public class TaskUser
{
    public Guid TaskId { get; set; }
    public TaskEntity Task { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
}