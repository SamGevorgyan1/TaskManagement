using System.Text;
using TaskManagement.Application.Abstractions.Interfaces;

namespace TaskManagement.Infrastructure.Authentication;

public class PasswordHasher : IPasswordHasher
{
    public string Hash(string password)
    {
        return Convert.ToBase64String(Encoding.UTF8.GetBytes(password));
    }

    public bool Verify(string passwordHash, string password)
    {
        return passwordHash == Convert.ToBase64String(Encoding.UTF8.GetBytes(password));
    }
}