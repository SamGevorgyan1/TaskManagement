using AutoMapper;
using TaskManagement.Application.Users.Command;
using TaskManagement.Domain.Entity;

namespace TaskManagement.Application.Mappings;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<User, RegisterCommand>();
        CreateMap<RegisterCommand, User>();
    }
}