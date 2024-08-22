using Microsoft.EntityFrameworkCore;
using TaskManagement.Domain.Entity;
using TaskManagement.Domain.Repository;
using TaskManagement.Infrastructure.Data;

namespace TaskManagement.Infrastructure.Repository;

public class TaskRepository : ITaskRepository
{
    private readonly TaskManagementDbContext _context;

    public TaskRepository(TaskManagementDbContext context)
    {
        _context = context;
    }

    public async Task<TaskEntity> GetByIdAsync(Guid id)
    {
        return await _context.Tasks
            .Include(t => t.Author)
            .Include(t => t.TaskUsers)
            .ThenInclude(tu => tu.User)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<List<TaskEntity>> GetAllAsync(Guid userId)
    {
        return await _context.Tasks
            .Include(t => t.Author)
            .Include(t => t.TaskUsers)
            .ThenInclude(tu => tu.User)
            .Where(t => t.AuthorId == userId || t.TaskUsers.Any(tu => tu.UserId == userId))
            .ToListAsync();
    }

    public async Task AddAsync(TaskEntity task)
    {
        await _context.Tasks.AddAsync(task);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(TaskEntity task)
    {
        _context.Tasks.Update(task);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(TaskEntity task)
    {
        _context.Tasks.Remove(task);
        await _context.SaveChangesAsync();
    }
}