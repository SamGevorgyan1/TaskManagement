using TaskManagement.Application.Exceptions;

namespace TaskManagement.Application.Users.Exceptions;

public class UserResourceAlreadyExistsException : ResourceAlreadyExistsException
{
    public UserResourceAlreadyExistsException(string message) : base(message)
    {
    }

    public UserResourceAlreadyExistsException(string message, Exception innerException) : base(message, innerException)
    {
    }
}