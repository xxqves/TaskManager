using TaskManager.Domain.Models;

namespace TaskManager.Application.Interfaces
{
    public interface IProjectRepository
    {
        Task<Guid> AddAsync(Project project, CancellationToken cancellationToken = default);

        Task<Project> GetProjectByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<List<Project>> GetProjectsAsync(CancellationToken cancellationToken = default);

        Task<Guid> UpdateAsync(Guid id, string name, Guid ownerId, CancellationToken cancellationToken = default);

        Task<Guid> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
