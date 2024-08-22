using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManagement.Domain.Entity;

namespace TaskManagement.Infrastructure.Configurations;

public class TaskUserConfiguration : IEntityTypeConfiguration<TaskUser>
{
    public void Configure(EntityTypeBuilder<TaskUser> builder)
    {
        builder.HasKey(tu => new { tu.TaskId, tu.UserId });

        builder.HasOne(tu => tu.Task)
            .WithMany(t => t.TaskUsers)
            .HasForeignKey(tu => tu.TaskId);

        builder.HasOne(tu => tu.User)
            .WithMany(u => u.TaskUsers)
            .HasForeignKey(tu => tu.UserId);
    }
}