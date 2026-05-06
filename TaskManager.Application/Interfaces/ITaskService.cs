using TaskManager.Application.Contracts.TaskItem;
using TaskManager.Domain.Models;

namespace TaskManager.Application.Interfaces
{
    public interface ITaskService
    {
        Task<Guid> CreateAsync(CreateTaskRequest request, CancellationToken cancellationToken = default);

        Task<List<TaskItem>> GetByProjectAsync(Guid projectId, CancellationToken cancellationToken = default);

        Task<Guid> UpdateAsync(UpdateTaskRequest request, CancellationToken cancellationToken = default);

        Task<Guid> ChangeStatusAsync(Guid id, CancellationToken cancellationToken = default);

        Task<Guid> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
