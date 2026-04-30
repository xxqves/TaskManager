using TaskManager.Application.Contracts.Project;
using TaskManager.Application.Interfaces;
using TaskManager.Domain.Models;

namespace TaskManager.Application.Services
{
    public class ProjectService : IProjectService
    {
        public async Task<Guid> CreateAsync(CreateProjectRequest request, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<Guid> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Project>> GetMyAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<Guid> UpdateAsync(Guid id, UpdateProjectRequest request, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
