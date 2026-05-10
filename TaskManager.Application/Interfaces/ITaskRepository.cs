using TaskManager.Domain.Models;

namespace TaskManager.Application.Interfaces
{
    public interface ITaskRepository
    {
        Task<List<TaskItem>> GetByProjectAsync(Guid projectId, CancellationToken cancellationToken = default);

        Task<TaskItem> GetTaskByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<Guid> AddAsync(TaskItem item, CancellationToken cancellationToken = default);

        Task<Guid> UpdateAsync(Guid id, string title, string description, Domain.Enums.TaskStatus taskStatus, CancellationToken cancellationToken = default);

        Task<Guid> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
