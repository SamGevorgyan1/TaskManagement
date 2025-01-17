namespace TaskManagement.Application.Abstractions.Interfaces;

public interface IPasswordHasher
{
    string Hash(string password);
    bool Verify(string passwordHash, string password);
}