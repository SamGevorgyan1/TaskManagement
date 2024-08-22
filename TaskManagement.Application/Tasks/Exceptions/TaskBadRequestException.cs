using TaskManagement.Application.Exceptions;

namespace TaskManagement.Application.Tasks.Exceptions;

public class TaskBadRequestException : BadRequestException
{
    public TaskBadRequestException(string message) : base(message)
    {
    }

    public TaskBadRequestException(string message, Exception innerException) : base(message, innerException)
    {
    }
}