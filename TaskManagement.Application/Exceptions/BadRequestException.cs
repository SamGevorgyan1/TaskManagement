using System.Net;

namespace TaskManagement.Application.Exceptions;

public class BadRequestException : AppException
{
    public BadRequestException(string message) : base(message, HttpStatusCode.BadRequest)
    {
    }

    public BadRequestException(string message, Exception innerException) : base(message, innerException,
        HttpStatusCode.BadRequest)
    {
    }
}