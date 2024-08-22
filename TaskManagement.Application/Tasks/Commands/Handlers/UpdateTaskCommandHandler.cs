using AutoMapper;
using Convey.CQRS.Commands;
using TaskManagement.Application.Abstractions.Interfaces;
using TaskManagement.Application.Exceptions;
using TaskManagement.Application.Tasks.Exceptions;
using TaskManagement.Domain.Entity;
using TaskManagement.Domain.Enum;
using TaskManagement.Domain.Repository;

namespace TaskManagement.Application.Tasks.Commands.Handlers;

public class UpdateTaskCommandHandler : ICommandHandler<UpdateTaskCommand>
{
    private readonly ITaskRepository _taskRepository;
    private readonly IUserAccessor _userAccessor;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UpdateTaskCommandHandler(IUserAccessor userAccessor, IMapper mapper, ITaskRepository taskRepository,
        IUserRepository userRepository)
    {
        _userAccessor = userAccessor;
        _mapper = mapper;
        _taskRepository = taskRepository;
        _userRepository = userRepository;
    }

    public async Task HandleAsync(UpdateTaskCommand request, CancellationToken cancellationToken = default)
    {
        var task = await _taskRepository.GetByIdAsync(request.Id);
        if (task == null)
        {
            throw new TaskNotFoundException("Task not found with the provided ID");
        }

        var user = await _userAccessor.GetCurrentUser();

        if (request.AssigneeEmails != null && request.AssigneeEmails.Count != 0)
        {
            task.TaskUsers.Clear();
            var assignees = await GetAssigneesAsync(request.AssigneeEmails);
            task.TaskUsers = assignees.Select(assignee => new TaskUser
            {
                TaskId = task.Id,
                UserId = assignee.Id
            }).ToList();
        }
        task.Title = request.Title;
        task.Description = request.Description;
        task.Status = request.Status;
        task.Priority = request.Priority;
        task.DueDate = request.DueDate;
        if (user.Role == UserRole.SuperAdmin || CheckPermission(user, task))
        {
            await _taskRepository.UpdateAsync(task);
        }
        else
        {
            throw new TaskForbiddenException("You don't have permission to access this resource");
        }
    }

    private bool CheckPermission(User user, TaskEntity taskEntity)
    {
        if (user.Id == taskEntity.AuthorId)
        {
            return true;
        }
        var isUserInTaskUsers = taskEntity.TaskUsers
            .Any(taskUser => taskUser.UserId == user.Id);
            
        return isUserInTaskUsers;
    }

    private async Task<List<User>> GetAssigneesAsync(List<string> assigneeEmails)
    {
        var assignees = await _userRepository.GetUsersByEmailsAsync(assigneeEmails);
        var missingEmails = assigneeEmails.Except(assignees.Select(u => u.Email)).ToList();

        if (missingEmails.Any())
        {
            throw new NotFoundException(
                $"The following emails are not associated with any users: {string.Join(", ", missingEmails)}");
        }

        return assignees;
    }
}