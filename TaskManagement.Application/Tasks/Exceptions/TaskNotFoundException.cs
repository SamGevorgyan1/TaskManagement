using TaskManagement.Application.Exceptions;

namespace TaskManagement.Application.Tasks.Exceptions;

public class TaskNotFoundException : NotFoundException
{
    public TaskNotFoundException(string message) : base(message)
    {
    }

    public TaskNotFoundException(string message, Exception innerException) : base(message, innerException)
    {
    }
}