using TaskManager.Application.Contracts.Project;
using TaskManager.Application.Interfaces;
using TaskManager.Domain.Models;

namespace TaskManager.Application.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly ICurrentUserService _currentUserService;

        public ProjectService(IProjectRepository projectRepository, ICurrentUserService currentUserService)
        {
            _projectRepository = projectRepository;
            _currentUserService = currentUserService;
        }

        public async Task<Guid> CreateAsync(CreateProjectRequest request, CancellationToken cancellationToken = default)
        {
            var project = Project.Create(
                Guid.NewGuid(),
                request.Name,
                _currentUserService.UserId
            );

            return await _projectRepository.AddAsync(project, cancellationToken);
        }

        public async Task<Guid> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var project = await _projectRepository.GetProjectByIdAsync(id, cancellationToken);

            if (project == null)
            {
                throw new Exception("Project not found");
            }

            if (project.OwnerId != _currentUserService.UserId && _currentUserService.Role != "Admin")
            {
                throw new Exception("Forbidden");
            }

            return await _projectRepository.DeleteAsync(id, cancellationToken);
        }

        public async Task<List<Project>> GetMyAsync(CancellationToken cancellationToken = default)
        {
            return await _projectRepository.GetProjectsAsync(cancellationToken);
        }

        public async Task<Guid> UpdateAsync(Guid id, UpdateProjectRequest request, CancellationToken cancellationToken = default)
        {
            var project = await _projectRepository.GetProjectByIdAsync(id, cancellationToken);

            if (project == null)
            {
                throw new Exception("Project not found");
            }

            if (project.OwnerId != _currentUserService.UserId && _currentUserService.Role != "Admin")
            {
                throw new Exception("Forbidden");
            }

            return await _projectRepository.UpdateAsync(id, request.Name, request.OwnerId, cancellationToken);
        }
    }
}
