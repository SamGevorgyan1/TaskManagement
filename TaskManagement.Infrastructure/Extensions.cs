using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using TaskManagement.Domain.Repository;
using TaskManagement.Infrastructure.Data;
using TaskManagement.Infrastructure.Repository;

namespace TaskManagement.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure
        (this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<TaskManagementDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("TaskManagementDbContext"),
                b => b.MigrationsAssembly("data_access_layer")));
        services.AddTransient<ITaskRepository, TaskRepository>();
        services.AddTransient<IUserRepository, UserRepository>();
        return services;
    }
}