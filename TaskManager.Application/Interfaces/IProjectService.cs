using TaskManager.Application.Contracts.Project;
using TaskManager.Domain.Models;

namespace TaskManager.Application.Interfaces
{
    public interface IProjectService
    {
        Task<Guid> CreateAsync(CreateProjectRequest request, CancellationToken cancellationToken = default);

        Task<List<Project>> GetMyAsync(CancellationToken cancellationToken = default);

        Task<Guid> UpdateAsync(Guid id, UpdateProjectRequest request, CancellationToken cancellationToken = default);

        Task<Guid> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
