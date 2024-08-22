using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TaskManagement.Infrastructure.Data;

public class TaskManagementDbContextFactory : IDesignTimeDbContextFactory<TaskManagementDbContext>
{
    public TaskManagementDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<TaskManagementDbContext>();
        optionsBuilder.UseNpgsql("Host=localhost;Database=task_management;Username=admin;Password=Sam158@_");
        return new TaskManagementDbContext(optionsBuilder.Options);
    }
}