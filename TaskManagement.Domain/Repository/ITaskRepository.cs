using TaskManagement.Domain.Entity;

namespace TaskManagement.Domain.Repository
{
    public interface ITaskRepository
    {
        Task<TaskEntity> GetByIdAsync(Guid id);
        Task<List<TaskEntity>> GetAllAsync(Guid userId);
        Task AddAsync(TaskEntity task);
        Task UpdateAsync(TaskEntity task);
        Task DeleteAsync(TaskEntity task);
    }
}