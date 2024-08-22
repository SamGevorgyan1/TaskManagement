using AutoMapper;
using TaskManagement.Application.DTOs;
using TaskManagement.Application.Tasks.Commands;
using TaskManagement.Domain.Entity;

namespace TaskManagement.Application.Mappings;

public class TaskMappingProfile : Profile
{
    public TaskMappingProfile()
    {
        CreateMap<TaskEntity, TaskDto>()
            .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author.Email))
            .ForMember(dest => dest.AssigneeEmails,
                opt => opt.MapFrom(src => src.TaskUsers.Select(tu => tu.User.Email).ToList()));
        CreateMap<CreateTaskCommand, TaskEntity>();
    }
}