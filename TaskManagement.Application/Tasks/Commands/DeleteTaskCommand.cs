using Convey.CQRS.Commands;

namespace TaskManagement.Application.Tasks.Commands;

public class DeleteTaskCommand : ICommand
{
    public Guid Id { get; set; }

    public DeleteTaskCommand(Guid id)
    {
        Id = id;
    }
}