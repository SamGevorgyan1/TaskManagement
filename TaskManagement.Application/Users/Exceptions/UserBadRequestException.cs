using TaskManagement.Application.Exceptions;

namespace TaskManagement.Application.Users.Exceptions;

public class UserBadRequestException:BadRequestException
{
    public UserBadRequestException(string message) : base(message)
    {
    }

    public UserBadRequestException(string message, Exception innerException) : base(message, innerException)
    {
    }
}