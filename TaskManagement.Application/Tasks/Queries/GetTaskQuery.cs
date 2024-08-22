using Convey.CQRS.Queries;
using TaskManagement.Application.DTOs;

namespace TaskManagement.Application.Tasks.Queries;

public class GetTaskQuery : IQuery<TaskDto>
{
    public Guid Id { get; set; }

    public GetTaskQuery(Guid id)
    {
        Id = id;
    }
}