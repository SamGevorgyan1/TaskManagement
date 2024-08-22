using System.Net;
using Convey.CQRS.Commands;
using Convey.CQRS.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.Tasks.Commands;
using TaskManagement.Application.Tasks.Queries;

namespace TaskManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TaskController : ControllerBase
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IQueryDispatcher _queryDispatcher;


    public TaskController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
    {
        _commandDispatcher = commandDispatcher;
        _queryDispatcher = queryDispatcher;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTaskCommand createTask)
    {
        await _commandDispatcher.SendAsync(createTask);
        return StatusCode((int)HttpStatusCode.Created);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTaskCommand command)
    {
        command.Id = id;
        await _commandDispatcher.SendAsync(command);
        return Ok(command);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var task = await _queryDispatcher.QueryAsync(new GetTaskQuery(id));
        return Ok(task);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var tasks = await _queryDispatcher.QueryAsync(new GetTaskListQuery());
        return Ok(tasks);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _commandDispatcher.SendAsync(new DeleteTaskCommand(id));
        return NoContent();
    }
}