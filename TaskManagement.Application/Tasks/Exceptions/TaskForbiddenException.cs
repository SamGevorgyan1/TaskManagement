using TaskManagement.Application.Exceptions;

namespace TaskManagement.Application.Tasks.Exceptions;

public class TaskForbiddenException : ForbiddenException
{
    public TaskForbiddenException(string message) : base(message)
    {
    }

    public TaskForbiddenException(string message, Exception innerException) : base(message, innerException)
    {
    }
}