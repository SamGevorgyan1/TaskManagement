using Convey.WebApi.Requests;
using TaskManagement.Application.Abstractions;
using TaskManagement.Application.Abstractions.Interfaces;
using TaskManagement.Application.DTOs;
using TaskManagement.Application.Users.Exceptions;
using TaskManagement.Domain.Repository;

namespace TaskManagement.Application.Users.Request.Handler;

public class LoginRequestHandler : IRequestHandler<LoginRequest, LoginResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtProvider _jwtProvider;
    private readonly IPasswordHasher _passwordHasher;

    public LoginRequestHandler(IUserRepository userRepository, IJwtProvider jwtProvider, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _jwtProvider = jwtProvider;
        _passwordHasher = passwordHasher;
    }

    public async Task<LoginResponse> HandleAsync(LoginRequest query,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(query.Email);
        if (user == null || !_passwordHasher.Verify(user.PasswordHash, query.Password))
        {
            throw new UserBadRequestException("Wrong email or password");
        }

        var accessToken = _jwtProvider.Generate(user);
        return new LoginResponse { AccessToken = accessToken };
    }
}