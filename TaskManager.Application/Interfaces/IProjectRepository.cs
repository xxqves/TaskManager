using TaskManager.Domain.Models;

namespace TaskManager.Application.Interfaces
{
    public interface IProjectRepository
    {
        Task<List<Project>> GetUserProjectsAsync(Guid userId, CancellationToken cancellationToken = default);

        Task<Project> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<Guid> AddAsync(Project project, CancellationToken cancellationToken = default);
    }
}
