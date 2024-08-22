using System.Reflection;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Domain.Entity;
using TaskManagement.Infrastructure.Configurations;

namespace TaskManagement.Infrastructure.Data;

public class TaskManagementDbContext : DbContext
{
    public TaskManagementDbContext(DbContextOptions<TaskManagementDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<TaskEntity> Tasks { get; set; }
    public DbSet<TaskUser> TaskUsers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new TaskConfiguration());
        modelBuilder.ApplyConfiguration(new TaskUserConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
    }
}