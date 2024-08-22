using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManagement.Domain.Entity;

namespace TaskManagement.Infrastructure.Configurations;

public class TaskConfiguration : IEntityTypeConfiguration<TaskEntity>
{
    public void Configure(EntityTypeBuilder<TaskEntity> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(t => t.Description)
            .HasMaxLength(1000);

        builder.Property(t => t.Status)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(t => t.Priority)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(t => t.DueDate)
            .IsRequired(false);

        builder.HasOne<User>(t => t.Author)
            .WithMany()
            .HasForeignKey(t => t.AuthorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(t => t.TaskUsers)
            .WithOne(tu => tu.Task)
            .HasForeignKey(tu => tu.TaskId);
    }
}