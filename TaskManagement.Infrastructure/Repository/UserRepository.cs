using Microsoft.EntityFrameworkCore;
using TaskManagement.Domain.Entity;
using TaskManagement.Domain.Repository;
using TaskManagement.Infrastructure.Data;

namespace TaskManagement.Infrastructure.Repository;

public class UserRepository : IUserRepository
{
    private readonly TaskManagementDbContext _context;

    public UserRepository(TaskManagementDbContext context)
    {
        _context = context;
    }

    public async Task<User> CreateAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<List<User>> GetAllAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<User> GetByIdAsync(Guid id)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public Task<User> UpdateAsync(Guid id, User user)
    {
        throw new NotImplementedException();
    }

    public async Task<List<User>> GetUsersByEmailsAsync(List<string> emails)
    {
        return await _context.Users
            .Where(u => emails.Contains(u.Email))
            .ToListAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
         await _context.Users
             .Where(t => t.Id == id)
            .ExecuteDeleteAsync();
    }
}