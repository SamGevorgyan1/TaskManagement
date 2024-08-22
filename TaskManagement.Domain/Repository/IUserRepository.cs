using TaskManagement.Domain.Entity;

namespace TaskManagement.Domain.Repository;

public interface IUserRepository
{
    Task<User> CreateAsync(User user);

    Task<List<User>> GetAllAsync();

    Task<User> GetByIdAsync(Guid id);
    
    Task<User?> GetByEmailAsync(string email);

    Task<User> UpdateAsync(Guid id, User user);
    
    Task<List<User>> GetUsersByEmailsAsync(List<string> emails);

    Task DeleteAsync(Guid id);
}