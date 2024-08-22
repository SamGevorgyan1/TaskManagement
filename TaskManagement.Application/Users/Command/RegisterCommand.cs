using Convey.CQRS.Commands;
using TaskManagement.Domain.Enum;

namespace TaskManagement.Application.Users.Command;

public class RegisterCommand : ICommand
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public UserRole? Role { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}