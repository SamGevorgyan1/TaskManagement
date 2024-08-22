using AutoMapper;
using Convey.CQRS.Commands;
using TaskManagement.Application.Abstractions.Interfaces;
using TaskManagement.Application.Users.Exceptions;
using TaskManagement.Domain.Entity;
using TaskManagement.Domain.Repository;

namespace TaskManagement.Application.Users.Command.Handlers;

public class RegisterCommandHandler : ICommandHandler<RegisterCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IPasswordHasher _passwordHasher;

    public RegisterCommandHandler(IUserRepository userRepository, IMapper mapper, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _passwordHasher = passwordHasher;
    }

    public async Task HandleAsync(RegisterCommand command, CancellationToken cancellationToken)
    {
        var byEmail = await _userRepository.GetByEmailAsync(command.Email);

        if (byEmail != null)
        {
            throw new UserResourceAlreadyExistsException("User already exists with the email");
        }

        var user = _mapper.Map<RegisterCommand, User>(command);
        user.PasswordHash = _passwordHasher.Hash(command.Password);
        await _userRepository.CreateAsync(user);
    }
}