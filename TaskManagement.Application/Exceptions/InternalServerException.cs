using System.Net;

namespace TaskManagement.Application.Exceptions;

public class InternalServerException : AppException
{
    public InternalServerException(string message) : base(message, HttpStatusCode.InternalServerError)
    {
    }

    public InternalServerException(string message, Exception innerException) : base(message, innerException,
        HttpStatusCode.InternalServerError)
    {
    }
}