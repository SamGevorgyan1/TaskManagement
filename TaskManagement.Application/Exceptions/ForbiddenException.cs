using System.Net;

namespace TaskManagement.Application.Exceptions;

public class ForbiddenException : AppException
{
    public ForbiddenException(string message) : base(message, HttpStatusCode.Forbidden)
    {
    }

    public ForbiddenException(string message, Exception innerException) : base(message, innerException, HttpStatusCode.Forbidden)
    {
    }
}