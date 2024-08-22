using Convey.WebApi.Requests;

namespace TaskManagement.Application.Users.Request;

public class LoginRequest : IRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
}