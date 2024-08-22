using System.Net;

namespace TaskManagement.Application.Exceptions;

public class ResourceAlreadyExistsException : AppException
{
    public ResourceAlreadyExistsException(string message) : base(message, HttpStatusCode.Conflict)
    {
    }

    public ResourceAlreadyExistsException(string message, Exception innerException) : base(message, innerException, HttpStatusCode.Conflict)
    {
    }
}