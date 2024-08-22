using System.Reflection;
using Convey;
using Convey.CQRS.Commands;
using Convey.CQRS.Queries;
using Convey.WebApi;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskManagement.Application.Tasks.Commands;
using TaskManagement.Application.Tasks.Validator;
using TaskManagement.Application.Users.Command;
using TaskManagement.Application.Users.Request;
using TaskManagement.Application.Users.Validators;

namespace TaskManagement.Application;

public static class Extensions
{
    public static IServiceCollection AddApplication
        (this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddFluentValidationAutoValidation();
        services.AddScoped<IValidator<RegisterCommand>, RegisterCommandValidator>();
        services.AddScoped<IValidator<LoginRequest>, LoginRequestValidator>();
        services.AddScoped<IValidator<CreateTaskCommand>, CreateTaskCommandValidator>();
        services.AddScoped<IValidator<UpdateTaskCommand>, UpdateTaskCommandValidator>();
        services
            .AddConvey()
            .AddInMemoryCommandDispatcher()
            .AddInMemoryQueryDispatcher()
            .AddCommandHandlers()
            .AddQueryHandlers()
            .AddWebApi()
            .Build();
        return services;
    }
}