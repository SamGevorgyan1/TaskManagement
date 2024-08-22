using System.Net;
using Convey.CQRS.Commands;
using Convey.WebApi.Requests;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.DTOs;
using TaskManagement.Application.Users.Command;
using TaskManagement.Application.Users.Request;


namespace TaskManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IRequestDispatcher _requestDispatcher;


    public UserController(
        ICommandDispatcher commandDispatcher,
        IRequestDispatcher requestDispatcher
    )
    {
        _commandDispatcher = commandDispatcher;
        _requestDispatcher = requestDispatcher;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterCommand command)
    {
        await _commandDispatcher.SendAsync(command);
        return StatusCode((int)HttpStatusCode.Created);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var accessToken = await _requestDispatcher.DispatchAsync<LoginRequest, LoginResponse>(request);
        return Ok(accessToken);
    }
}