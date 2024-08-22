using System.Net;

namespace TaskManagement.Application.Exceptions;

public class NotFoundException : AppException
{
    public NotFoundException(string message) : base(message, HttpStatusCode.NotFound)
    {
    }

    public NotFoundException(string message, Exception innerException) : base(message, innerException, HttpStatusCode.NotFound)
    {
    }
}