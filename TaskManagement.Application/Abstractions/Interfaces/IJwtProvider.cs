using TaskManagement.Domain.Entity;

namespace TaskManagement.Application.Abstractions.Interfaces;

public interface IJwtProvider
{
    string Generate(User user);
}