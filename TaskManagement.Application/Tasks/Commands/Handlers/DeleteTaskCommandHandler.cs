using Convey.CQRS.Commands;
using TaskManagement.Application.Abstractions.Interfaces;
using TaskManagement.Application.Tasks.Exceptions;
using TaskManagement.Domain.Enum;
using TaskManagement.Domain.Repository;

namespace TaskManagement.Application.Tasks.Commands.Handlers;

public class DeleteTaskCommandHandler : ICommandHandler<DeleteTaskCommand>
{
    private readonly ITaskRepository _taskRepository;
    private readonly IUserAccessor _userAccessor;


    public DeleteTaskCommandHandler(ITaskRepository taskRepository, IUserAccessor userAccessor)
    {
        _taskRepository = taskRepository;
        _userAccessor = userAccessor;
    }

    public async Task HandleAsync(DeleteTaskCommand command, CancellationToken cancellationToken)
    {
        var task = await _taskRepository.GetByIdAsync(command.Id);
        var user = await _userAccessor.GetCurrentUser();
        if (task == null)
        {
            throw new TaskNotFoundException("Task not found the provided ID");
        }

        if (user.Role == UserRole.SuperAdmin || task.AuthorId == user.Id)
        {
            await _taskRepository.DeleteAsync(task);
        }

        throw new TaskForbiddenException("You don't have permission to access this resource");
    }
}