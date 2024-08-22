using TaskManagement.Application.Exceptions;

namespace TaskManagement.Application.Tasks.Exceptions;

public class TaskInternalServerException : InternalServerException
{
    public TaskInternalServerException(string message) : base(message)
    {
    }

    public TaskInternalServerException(string message, Exception innerException) : base(message, innerException)
    {
    }
}