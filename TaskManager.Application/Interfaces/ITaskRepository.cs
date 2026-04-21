using TaskManager.Domain.Models;

namespace TaskManager.Application.Interfaces
{
    public interface ITaskRepository
    {
        Task<List<TaskItem>> GetByProjectAsync(Guid projectId, CancellationToken cancellationToken = default);

        Task<Guid> AddAsync(TaskItem item, CancellationToken cancellationToken = default);
    }
}
