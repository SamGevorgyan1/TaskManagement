using AutoMapper;
using Convey.CQRS.Queries;
using TaskManagement.Application.Abstractions;
using TaskManagement.Application.Abstractions.Interfaces;
using TaskManagement.Application.DTOs;
using TaskManagement.Domain.Repository;

namespace TaskManagement.Application.Tasks.Queries.Handlers;

public class GetTaskListQueryHandler : IQueryHandler<GetTaskListQuery, List<TaskDto>>
{
    private readonly IUserAccessor _userAccessor;
    private readonly ITaskRepository _taskRepository;
    private readonly IMapper _mapper;

    public GetTaskListQueryHandler(IUserAccessor userAccessor, ITaskRepository taskRepository, IMapper mapper)
    {
        _userAccessor = userAccessor;
        _taskRepository = taskRepository;
        _mapper = mapper;
    }

    public async Task<List<TaskDto>> HandleAsync(GetTaskListQuery query, CancellationToken cancellationToken = default)
    {
        var userId = (await _userAccessor.GetCurrentUser()).Id;
        var tasks = await _taskRepository.GetAllAsync(userId);
        return _mapper.Map<List<TaskDto>>(tasks);
    }
}