using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using TaskManagement.Application.Abstractions.Interfaces;
using TaskManagement.Domain.Entity;
using TaskManagement.Domain.Repository;

namespace TaskManagement.Infrastructure.Authentication;

public class UserAccessor : IUserAccessor
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IUserRepository _userRepository;

    public UserAccessor(IHttpContextAccessor httpContextAccessor, IUserRepository userRepository)
    {
        _httpContextAccessor = httpContextAccessor;
        _userRepository = userRepository;
    }

    public async Task<User> GetCurrentUser()
    {
        var userEmail = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Email);
        return await _userRepository.GetByEmailAsync(userEmail);
    }
}