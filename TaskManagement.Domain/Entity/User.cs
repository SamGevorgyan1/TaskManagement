using TaskManagement.Domain.Enum;

namespace TaskManagement.Domain.Entity;

public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public UserRole? Role { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public ICollection<TaskUser> TaskUsers { get; set; } = new List<TaskUser>();
}