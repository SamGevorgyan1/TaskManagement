using AutoMapper;
using Convey.CQRS.Queries;
using TaskManagement.Application.Abstractions;
using TaskManagement.Application.Abstractions.Interfaces;
using TaskManagement.Application.DTOs;
using TaskManagement.Application.Tasks.Exceptions;
using TaskManagement.Domain.Entity;
using TaskManagement.Domain.Enum;
using TaskManagement.Domain.Repository;

namespace TaskManagement.Application.Tasks.Queries.Handlers;

public class GetTaskQueryHandler : IQueryHandler<GetTaskQuery, TaskDto>
{
    private readonly ITaskRepository _taskRepository;
    private readonly IUserAccessor _userAccessor;
    private readonly IMapper _mapper;

    public GetTaskQueryHandler(ITaskRepository taskRepository, IUserAccessor userAccessor, IMapper mapper)
    {
        _taskRepository = taskRepository;
        _userAccessor = userAccessor;
        _mapper = mapper;
    }

    public async Task<TaskDto> HandleAsync(GetTaskQuery query, CancellationToken cancellationToken)
    {
        var task = await _taskRepository.GetByIdAsync(query.Id);
        var currentUser = await _userAccessor.GetCurrentUser();

        if (task == null)
        {
            throw new TaskNotFoundException("Task not found with given ID");
        }

        if (currentUser.Role == UserRole.SuperAdmin || CheckPermission(currentUser, task))
        {
            return _mapper.Map<TaskEntity, TaskDto>(task);
        }

        throw new TaskForbiddenException("You don't have permission to access this resource");
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
}