using TaskManagement.Domain.Entity;

namespace TaskManagement.Application.Abstractions.Interfaces;

public interface IUserAccessor
{
    Task<User> GetCurrentUser();
}