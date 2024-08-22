using TaskManagement.Application.Exceptions;

namespace TaskManagement.Application.Tasks.Exceptions;

public class TaskResourceAlreadyExistsException : ResourceAlreadyExistsException
{
    public TaskResourceAlreadyExistsException(string message) : base(message)
    {
    }

    public TaskResourceAlreadyExistsException(string message, Exception innerException) : base(message, innerException)
    {
    }
}