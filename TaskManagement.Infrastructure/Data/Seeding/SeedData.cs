using TaskManagement.Domain.Entity;
using TaskManagement.Domain.Enum;
using TaskStatus = TaskManagement.Domain.Enum.TaskStatus;

namespace TaskManagement.Infrastructure.Data.Seeding;

public static class SeedData
{
    public static void Seed(TaskManagementDbContext context)
    {
            
        if (!context.Users.Any())
        {
               
            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = "User",
                Surname = "User",
                Role = UserRole.SuperAdmin,
                Email = "user@example.com",
                PasswordHash = "password",
            };

            context.Users.Add(user);
            context.SaveChanges();
                
            var task = new TaskEntity
            {
                Id = Guid.NewGuid(),
                Title = "STask",
                Description = "Task",
                Status = TaskStatus.Completed,
                Priority = TaskPriority.Medium,
                CreatedAt = DateTime.UtcNow,
                DueDate = DateTime.UtcNow.AddDays(7),
                AuthorId = user.Id,
            };

            context.Tasks.Add(task);
            context.SaveChanges();
        }
    }
}