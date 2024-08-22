using AutoMapper;
using Convey.CQRS.Commands;
using TaskManagement.Application.Abstractions.Interfaces;
using TaskManagement.Application.Exceptions;
using TaskManagement.Domain.Entity;
using TaskManagement.Domain.Repository;

namespace TaskManagement.Application.Tasks.Commands.Handlers;

public class CreateTaskCommandHandler : ICommandHandler<CreateTaskCommand>
{
    private readonly ITaskRepository _taskRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUserAccessor _userAccessor;
    private readonly IMapper _mapper;

    public CreateTaskCommandHandler(
        ITaskRepository taskRepository,
        IUserRepository userRepository,
        IUserAccessor userAccessor,
        IMapper mapper)
    {
        _taskRepository = taskRepository;
        _userRepository = userRepository;
        _userAccessor = userAccessor;
        _mapper = mapper;
    }

    public async Task HandleAsync(CreateTaskCommand command, CancellationToken cancellationToken)
    {
        var user = await _userAccessor.GetCurrentUser();
        var task = _mapper.Map<TaskEntity>(command);

        if (command.AssigneeEmails != null && command.AssigneeEmails.Count != 0)
        {
            var assignees = await GetAssigneesAsync(command.AssigneeEmails);
            task.TaskUsers = assignees.Select(assignee => new TaskUser
            {
                TaskId = task.Id,
                UserId = assignee.Id
            }).ToList();
        }

        task.AuthorId = user.Id;
        await _taskRepository.AddAsync(task);
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